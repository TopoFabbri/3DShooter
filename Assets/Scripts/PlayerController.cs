using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 500f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float grabDis = 3f;
    [SerializeField] private float barrelDis = 2f;
    [SerializeField] private string gunTag = "Gun";

    [Header("Objects:")] [SerializeField] private Gun weapon;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Hud hud;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private Id stateId;
    [SerializeField] private GameObject lethalPrefab;

    private Vector3 movement;
    private bool ads;
    private bool hasShot;

    public static event Action Destroyed;

    public Gun getWeapon => weapon;

    private void OnEnable()
    {
        // Action subscriptions
        InputListener.Move += OnMove;
        InputListener.Shoot += OnShoot;
        InputListener.Aim += OnAim;
        InputListener.Drop += OnDrop;
        InputListener.Grab += OnGrab;
        InputListener.Reload += OnReload;
        InputListener.DropLethal += OnDropLethal;
        stateMachine.Subscribe(stateId, OnUpdate);
    }

    private void OnDisable()
    {
        // Action unsubscription
        InputListener.Move -= OnMove;
        InputListener.Shoot -= OnShoot;
        InputListener.Aim -= OnAim;
        InputListener.Drop -= OnDrop;
        InputListener.Grab -= OnGrab;
        InputListener.Reload -= OnReload;
        InputListener.DropLethal -= OnDropLethal;
        stateMachine.UnSubscribe(stateId, OnUpdate);
    }

    private void FixedUpdate()
    {
        var trans = transform;
        rb.AddForce((movement.x * trans.right + movement.z * trans.forward) * Time.fixedDeltaTime,
            ForceMode.Acceleration);
        var tmp = ClampPlaneVelocity(rb.velocity, maxSpeed);
        rb.velocity = new Vector3(tmp.x, rb.velocity.y, tmp.y);
    }

    /// <summary>
    /// Gameplay-only update
    /// </summary>
    private void OnUpdate()
    {
        if (ads)
            AimStart();
        else
            AimStop();

        if (getWeapon)
        {
            if (getWeapon.isReloading)
            {
                var trans = transform;
                getWeapon.transform.position = trans.position - trans.forward;
            }
            else
            {
                Transform cameraMovementTransform;
                getWeapon.transform.position =
                    (cameraMovementTransform = cameraMovement.transform).TransformPoint(new Vector3(.26f, -.234f, .561f));
                getWeapon.transform.LookAt(cameraMovement.worldMouseDir + cameraMovementTransform.up * -0.1597f);
            }
        }

        hud.SetPickupTextActive(PointingAtGun(out _));
    }

    /// <summary>
    /// Call character move action
    /// </summary>
    /// <param name="input"></param>
    public void OnMove(InputValue input)
    {
        var direction = input.Get<Vector2>();
        movement = new Vector3(direction.x, 0, direction.y) * moveSpeed;
    }

    /// <summary>
    /// Call character shoot action
    /// </summary>
    public void OnShoot()
    {
        if (getWeapon)
            getWeapon.GetComponent<Gun>().Shoot();
    }

    /// <summary>
    /// Call character aim action
    /// </summary>
    /// <param name="input"></param>
    public void OnAim(InputValue input)
    {
        ads = input.isPressed;
    }

    /// <summary>
    /// Call character drop weapon action
    /// </summary>
    public void OnDrop()
    {
        if (getWeapon)
            getWeapon.DropGun();
        
        weapon = null;
    }

    /// <summary>
    /// Call character pickup weapon action
    /// </summary>
    public void OnGrab()
    {
        if (!PointingAtGun(out var hit))
            return;

        if (getWeapon)
        {
            getWeapon.DropGun();
            weapon = null;
        }

        hit.transform.gameObject.GetComponent<Gun>().GrabGun(transform);
        weapon = hit.transform.gameObject.GetComponent<Gun>();
    }

    /// <summary>
    /// Call character reload weapon action
    /// </summary>
    public void OnReload()
    {
        if (getWeapon)
            getWeapon.Reload();
    }

    /// <summary>
    /// Call place equipped lethal
    /// </summary>
    private void OnDropLethal()
    {
        var trans = transform;
        var position = trans.position;
        var rotation = trans.rotation;
        
        BarrelManager.SpawnBarrel(lethalPrefab, position + trans.forward * barrelDis, rotation);
    }

    /// <summary>
    /// Clamp x - z velocity
    /// </summary>
    /// <param name="vel"></param>
    /// <param name="clampValue"></param>
    /// <returns>x & z velocity normalized within 'clampValue'</returns>
    private static Vector2 ClampPlaneVelocity(Vector3 vel, float clampValue)
    {
        var res = new Vector2(vel.x, vel.z);
        res = Vector2.ClampMagnitude(res, clampValue);
        return res;
    }

    /// <summary>
    /// Set up state 'aiming'
    /// </summary>
    private void AimStart()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        cameraMovement.aimDownSight = true;
    }

    /// <summary>
    /// End character state 'aiming'
    /// </summary>
    private void AimStop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        cameraMovement.aimDownSight = false;
    }

    /// <summary>
    /// Check if can grab gun
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    private bool PointingAtGun(out RaycastHit hit)
    {
        var cam = cameraMovement.gameObject.transform;
        var ray = new Ray(cam.position, cam.forward);

        if (!Physics.Raycast(ray, out hit)) return false;

        return hit.transform.gameObject.CompareTag(gunTag) &&
               Vector3.Distance(hit.transform.position, transform.position) < grabDis;
    }

    /// <summary>
    /// Sets player velocity to 0
    /// </summary>
    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }
}