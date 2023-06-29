using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    //TODO: TP2 - Syntax - Fix declaration order
    [SerializeField] private float recoil = 10f;

    [Header("Objects:")][SerializeField] private Gun weapon;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private Hud hud;

    private ParticleSystem ps;
    private CameraMovement cameraMovement;
    private Rigidbody rb;
    private bool ads;
    private Vector3 movement;
    private bool hasShot = false;

    private void Start()
    {
        // Action subscriptions
        InputListener.Move += OnMove;
        InputListener.Shoot += OnShoot;
        InputListener.Aim += OnAim;
        InputListener.Drop += OnDrop;
        InputListener.Grab += OnGrab;
        InputListener.Reload += OnReload;
        
        cameraMovement = GetComponentInChildren<CameraMovement>();
        //TODO: Fix - Add [RequireComponentAttribute]
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce((movement.x * transform.right + movement.z * transform.forward) * Time.fixedDeltaTime,
            ForceMode.Acceleration);
        Vector2 tmp = ClampPlaneVelocity(rb.velocity, maxSpeed);
        rb.velocity = new Vector3(tmp.x, rb.velocity.y, tmp.y);
    }

    private void Update()
    {
        if (ads)
            AimStart();
        else
            AimStop();

        if (weapon)
        {
            if (weapon.GetReloading())
            {
                weapon.transform.position = transform.position - transform.forward;
            }
            else
            {
                weapon.transform.position = cameraMovement.transform.TransformPoint(new Vector3(.26f, -.234f, .561f));
                weapon.transform.LookAt(cameraMovement.GetWorldMouseDir() + cameraMovement.transform.up * -0.1597f);
            }
        }

        hud.SetTextActive(PointingAtGun());
    }

    //TODO: Fix - Using Input related logic outside of an input responsible class
    public void OnMove(InputValue input)
    {
        var direction = input.Get<Vector2>();
        movement = new Vector3(direction.x, 0, direction.y) * moveSpeed;
    }

    //TODO: Fix - Using Input related logic outside of an input responsible class
    public void OnShoot(InputValue value)
    {
        hasShot = value.isPressed;

        if (hasShot && weapon)
        {
            weapon.GetComponent<Gun>().Shoot();
            ps.Play();
        }
    }

    //TODO: Fix - Using Input related logic outside of an input responsible class
    public void OnAim(InputValue input)
    {
        ads = input.isPressed;
    }

    public void OnDrop()
    {
        weapon.DropGun();
        weapon = null;
        ps = null;
    }

    public void OnGrab()
    {
        Ray ray = new Ray(cameraMovement.gameObject.transform.position, cameraMovement.gameObject.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            //TODO: Fix - Hardcoded value
            if (hit.transform.gameObject.CompareTag("Gun") &&
                Vector3.Distance(hit.transform.position, transform.position) < 3f)
            {
                if (weapon)
                {
                    weapon.DropGun();
                    weapon = null;
                    ps = null;
                }

                hit.transform.gameObject.GetComponent<Gun>().GrabGun(transform);
                weapon = hit.transform.gameObject.GetComponent<Gun>();
                ps = weapon.GetComponentInChildren<ParticleSystem>();
            }
        }
    }

    public void OnReload()
    {
        if (weapon)
            weapon.Reload();
    }

    private Vector2 ClampPlaneVelocity(Vector3 vel, float clampValue)
    {
        Vector2 res = new Vector2(vel.x, vel.z);
        res = Vector2.ClampMagnitude(res, clampValue);
        return res;
    }

    private void AimStart()
    {
        Cursor.lockState = CursorLockMode.Confined;
        crosshair.SetActive(false);
        cameraMovement.ads = true;
    }

    private void AimStop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        crosshair.SetActive(true);
        cameraMovement.ads = false;
    }

    public Gun GetWeapon()
    {
        return weapon;
    }

    //TODO: TP2 - SOLID
    private void OnDestroy()
    {
        SceneManager.LoadScene(4);
    }

    private bool PointingAtGun()
    {
        Ray ray = new Ray(cameraMovement.gameObject.transform.position, cameraMovement.gameObject.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Gun") &&
                Vector3.Distance(hit.transform.position, transform.position) < 3f)
            {
                return true;
            }
        }

        return false;
    }
}