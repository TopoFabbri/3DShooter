using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float bordersMargin = 20f;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private Id stateId;

    private Camera cam;
    private Vector3 worldMouseDir;
    private float xRotation;

    public bool aimDownSight;

    private void Start()
    {
        InputListener.Camera += OnCamera;
        cam = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.Subscribe(stateId, OnUpdate);
    }
    
    private void OnDisable()
    {
        stateMachine.UnSubscribe(stateId, OnUpdate);
    }

    /// <summary>
    /// Gameplay-only update
    /// </summary>
    private void OnUpdate()
    {
        //TODO: TP2 - Strategy
        if (!aimDownSight)
            CenterAim();
        else
            CursorAim();
    }

    /// <summary>
    /// Manage aim when crosshair is on the middle
    /// </summary>
    private void CenterAim()
    {
        var camTransform = cam.transform;
        var ray = new Ray(camTransform.position, camTransform.forward);

        if (Physics.Raycast(ray, out var hit, 100f, ~(1 << 7)))
            worldMouseDir = hit.point;
        else
            worldMouseDir = camTransform.position + camTransform.forward * 100f;
    }

    /// <summary>
    /// Manage aim when crosshair is cursor
    /// </summary>
    private void CursorAim()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane + 5;
        var worldPos = cam.ScreenToWorldPoint(mousePos);
        var camPos = cam.transform.position;
        var worldMouse = new Ray(camPos, worldPos - camPos);

        if (Physics.Raycast(worldMouse, out var hit, 100f, ~(1 << 7)))
            worldMouseDir = hit.point;
        else
            worldMouseDir = worldMouse.origin + worldMouse.direction * 100f;

        var yRot = GetYRotFromMousePos(mousePos);
        xRotation -= GetXRotFromMousePos(mousePos);
        
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);

        playerBody.Rotate(Vector3.up, yRot);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    
    /// <summary>
    /// Get y rotation value if mouse is near screen borders
    /// </summary>
    /// <param name="mousePos"></param>
    /// <returns>float</returns>
    private float GetYRotFromMousePos(Vector3 mousePos)
    {
        float yRot = 0;
        var minX = bordersMargin;
        var maxX = Screen.width - bordersMargin;
        
        if (mousePos.x > maxX)
            yRot = (mousePos.x - maxX) / bordersMargin;
        else if (mousePos.x < minX)
            yRot = (mousePos.x - minX) / bordersMargin;
        
        return yRot * mouseSensitivity * Time.deltaTime;
    }

    /// <summary>
    /// Get x rotation value if mouse is near screen borders
    /// </summary>
    /// <param name="mousePos"></param>
    /// <returns>float</returns>
    private float GetXRotFromMousePos(Vector3 mousePos)
    {
        float xRot = 0;
        var minY = bordersMargin;
        var maxY = Screen.height - bordersMargin;

        if (mousePos.y > maxY)
            xRot = (mousePos.y - maxY) / bordersMargin;
        else if (mousePos.y < minY)
            xRot = (mousePos.y - minY) / bordersMargin;
        
        return xRot * mouseSensitivity * Time.deltaTime;
    }

    /// <summary>
    /// 'worldMouseDir' Getter
    /// </summary>
    /// <returns>Vector3</returns>
    public Vector3 GetWorldMouseDir()
    {
        return worldMouseDir;
    }

    /// <summary>
    /// Recieve camera input
    /// </summary>
    /// <param name="input"></param>
    public void OnCamera(InputValue input)
    {
        if (aimDownSight) return;
        
        var mousePos = input.Get<Vector2>();

        mousePos.x /= Screen.width;
        mousePos.y /= Screen.height;

        xRotation -= mousePos.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mousePos.x * mouseSensitivity);
    }
}