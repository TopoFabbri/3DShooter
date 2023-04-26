using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSpeed;

    [Header("Objects:")]
    [SerializeField] private Animator rifleAnim;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject scope;

    private Rigidbody rb;
    private bool shouldJump = false;
    private bool ads;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletSpawnPoint = GameObject.Find("BulletSpawn").transform;
        rifleAnim = GameObject.Find("Blaster").GetComponent<Animator>();
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

    public void OnShoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position + head.transform.forward, head.rotation);

        rifleAnim.SetTrigger("Shoot");
    }

    public void OnAim(InputValue input)
    {
        rifleAnim.SetBool("ADS", !rifleAnim.GetBool("ADS"));
        ads = input.isPressed;
    }

    public void OnDrop()
    {
        
    }

    private Vector2 ClampPlaneVelocity(Vector3 vel, float clampValue)
    {
        Vector2 res = new Vector2(vel.x, vel.z);
        res = Vector2.ClampMagnitude(res, clampValue);
        return res;
    }

    private void AimStart()
    {
        crosshair.SetActive(false);
        scope.SetActive(true);
    }

    private void AimStop()
    {
        crosshair.SetActive(true);
        scope.SetActive(false);
    }
}