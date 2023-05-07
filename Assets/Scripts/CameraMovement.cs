using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float bordersMargin = 20f;
    public bool ads = false;

    private Camera cam;
    private float xRotation = 0f;
    private Vector3 worldMouseDir;

    void Start()
    {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!ads)
            CenterAim();
        else
            CursorAim();
    }

    private void CenterAim()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        worldMouseDir = transform.forward;
    }

    private void CursorAim()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane + 5;
        RaycastHit hit = new RaycastHit();
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        Draw.Point(worldPos, 1f);
        Ray worldMouse = new Ray(cam.transform.position, worldPos - cam.transform.position);
        Debug.DrawRay(worldMouse.origin, worldMouse.direction);

        if (Physics.Raycast(worldMouse, out hit))
            Draw.Point(hit.transform.position, 1f);

        worldMouseDir = worldMouse.direction;

        float yRot = 0;
        
        if (mousePos.x > Screen.width - bordersMargin)
            yRot = ((mousePos.x - (Screen.width - bordersMargin)) / bordersMargin) * mouseSensitivity * Time.deltaTime;
        else if (mousePos.x < bordersMargin)
            yRot = ((mousePos.x - bordersMargin) / bordersMargin) * mouseSensitivity * Time.deltaTime;
        
        if (mousePos.y > Screen.height - bordersMargin)
            xRotation -= ((mousePos.y - (Screen.height - bordersMargin)) / bordersMargin) * mouseSensitivity * Time.deltaTime;
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
}