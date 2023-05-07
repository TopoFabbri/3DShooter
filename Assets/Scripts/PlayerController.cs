using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float recoil = 10f;

    [Header("Objects:")] [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject scope;
    [SerializeField] private ParticleSystem ps;

    private CameraMovement cameraMovement;
    private Rigidbody rb;
    private bool shouldJump = false;
    private bool ads;
    private Vector3 movement;
    private bool shot = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraMovement = GetComponentInChildren<CameraMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement.x * transform.right + movement.z * transform.forward, ForceMode.Acceleration);
        Vector2 tmp = ClampPlaneVelocity(rb.velocity, maxSpeed);
        rb.velocity = new Vector3(tmp.x, rb.velocity.y, tmp.y);

        if (shouldJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            shouldJump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.blue);

        if (ads)
            AimStart();
        else
            AimStop();

        if (Input.GetKey(KeyCode.Tab))
            SceneManager.LoadScene(0);

        if (weapon)
        {
            weapon.transform.position = cameraMovement.transform.TransformPoint(new Vector3(.26f, -.234f, .561f));
            weapon.transform.LookAt(weapon.transform.position + cameraMovement.GetWorldMouseDir());
        }
    }

    public void OnMove(InputValue input)
    {
        var direction = input.Get<Vector2>();
        movement = new Vector3(direction.x, 0, direction.y) * moveSpeed * Time.deltaTime;
    }

    public void OnJump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            shouldJump = true;
        }
    }

    public void OnShoot(InputValue value)
    {
        shot = value.isPressed;
        
        if (shot && weapon)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, weapon.transform.rotation);
            ps.Play();
        }
    }

    public void OnAim(InputValue input)
    {
        ads = input.isPressed;
    }

    public void OnDrop()
    {
        weapon.GetComponent<Gun>().DropGun();
        weapon = null;
    }

    public void OnGrab()
    {
        Ray ray = new Ray(cameraMovement.gameObject.transform.position, cameraMovement.gameObject.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Gun"))
            {
                hit.transform.gameObject.GetComponent<Gun>().GrabGun(transform);
                weapon = hit.transform.gameObject;
            }
        }
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
}