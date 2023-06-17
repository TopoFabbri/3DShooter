using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    //TODO: TP2 - Remove unused methods/variables/classes
    [SerializeField] private Transform head;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float bordersMargin = 20f;
    //TODO: Fix - Unclear name - Is this a standard feature in shooters?
    public bool ads = false;

    private Camera cam;
    private float xRotation = 0f;
    private Vector3 worldMouseDir;

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    void Start()
    {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //TODO: TP2 - Strategy
        if (!ads)
            CenterAim();
        else
            CursorAim();
    }

    private void CenterAim()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 100f, ~(1 << 7)))
            worldMouseDir = hit.point;
        else
            worldMouseDir = cam.transform.position + cam.transform.forward * 100f;
    }

    private void CursorAim()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane + 5;
        RaycastHit hit = new RaycastHit();
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        Ray worldMouse = new Ray(cam.transform.position, worldPos - cam.transform.position);

        if (Physics.Raycast(worldMouse, out hit, 100f, ~(1 << 7)))
            worldMouseDir = hit.point;
        else
            worldMouseDir = worldMouse.origin + worldMouse.direction * 100f;

        float yRot = 0;

        //TODO: Fix - Trash code
        if (mousePos.x > Screen.width - bordersMargin)
            yRot = ((mousePos.x - (Screen.width - bordersMargin)) / bordersMargin) * mouseSensitivity * Time.deltaTime;
        else if (mousePos.x < bordersMargin)
            yRot = ((mousePos.x - bordersMargin) / bordersMargin) * mouseSensitivity * Time.deltaTime;

        if (mousePos.y > Screen.height - bordersMargin)
            xRotation -= ((mousePos.y - (Screen.height - bordersMargin)) / bordersMargin) * mouseSensitivity *
                         Time.deltaTime;
        else if (mousePos.y < bordersMargin)
            xRotation -= ((mousePos.y - bordersMargin) / bordersMargin) * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up, yRot);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public Vector3 GetWorldMouseDir()
    {
        return worldMouseDir;
    }

    //TODO: Fix - Using Input related logic outside of an input responsible class
    public void OnCamera(InputValue input)
    {
        if (!ads)
        {
            //TODO: Fix - Unclear name
            Vector2 mouse = input.Get<Vector2>();

            mouse.x /= Screen.width;
            mouse.y /= Screen.height;

            xRotation -= mouse.y * mouseSensitivity;
            xRotation = Mathf.Clamp(xRotation, -89f, 89f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouse.x * mouseSensitivity);
        }
    }
}