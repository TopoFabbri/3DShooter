using Patterns;
using Patterns.SM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    /// <summary>
    /// Controller for camera
    /// </summary>
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity = 100f;
        [SerializeField] private float gamepadSensitivity = 1f;
        [SerializeField] private float cursorSensitivity = 10f;
        [SerializeField] private Transform playerBody;
        [SerializeField] private float bordersMargin = 20f;
        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private Id stateId;
    
        private const float CursorSpeed = 20f;

        private bool gamepad;
        private Camera cam;
        private float xRotation;
        private float yRotation;
        private Vector3 rotation;
        private Vector2 cursorVel;

        public bool aimDownSight;
    
        public Vector3 worldMouseDir { get; private set; }

        private void Start()
        {
            cam = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            InputListener.Camera += OnCamera;
            InputListener.GamepadCamera += OnGamepadCamera;
            stateMachine.Subscribe(stateId, OnUpdate);
        }

        private void OnDisable()
        {
            InputListener.Camera -= OnCamera;
            InputListener.GamepadCamera -= OnGamepadCamera;
            stateMachine.UnSubscribe(stateId, OnUpdate);
        }

        /// <summary>
        /// Gameplay-only update
        /// </summary>
        private void OnUpdate()
        {
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

            rotation.x = Mathf.Clamp(rotation.x - xRotation, -89f, 89f);

            playerBody.Rotate(Vector3.up, yRotation);
            transform.localRotation = Quaternion.Euler(rotation);

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
            Mouse.current.WarpCursorPosition((Vector2)Input.mousePosition + cursorVel);
        
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
            rotation.x -= GetXRotFromMousePos(mousePos);

            rotation.x = Mathf.Clamp(rotation.x, -89f, 89f);

            playerBody.Rotate(Vector3.up, yRot);
            transform.localRotation = Quaternion.Euler(rotation);
        }

        /// <summary>
        /// Get y rotation value if mouse is near screen borders
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns>Get camera y rotation</returns>
        private float GetYRotFromMousePos(Vector3 mousePos)
        {
            float yRot = 0;
            var minX = bordersMargin;
            var maxX = Screen.width - bordersMargin;

            if (mousePos.x > maxX)
                yRot = (mousePos.x - maxX) / bordersMargin;
            else if (mousePos.x < minX)
                yRot = (mousePos.x - minX) / bordersMargin;

            return yRot * cursorSensitivity * Time.deltaTime;
        }

        /// <summary>
        /// Get x rotation value if mouse is near screen borders
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns>Camera rotation on x axis</returns>
        private float GetXRotFromMousePos(Vector3 mousePos)
        {
            float xRot = 0;
            var minY = bordersMargin;
            var maxY = Screen.height - bordersMargin;

            if (mousePos.y > maxY)
                xRot = (mousePos.y - maxY) / bordersMargin;
            else if (mousePos.y < minY)
                xRot = (mousePos.y - minY) / bordersMargin;

            return xRot * cursorSensitivity * Time.deltaTime;
        }

        /// <summary>
        /// Receive camera input
        /// </summary>
        /// <param name="input"></param>
        private void OnCamera(InputValue input)
        {
            if (aimDownSight) return;

            var mouseInput = input.Get<Vector2>();

            xRotation = mouseInput.y * mouseSensitivity * Time.timeScale;
            yRotation = mouseInput.x * mouseSensitivity * Time.timeScale;
        }

        /// <summary>
        /// Take gamepad camera input
        /// </summary>
        /// <param name="input"></param>
        private void OnGamepadCamera(InputValue input)
        {
            var analogInput = input.Get<Vector2>();

            if (aimDownSight)
            {
                cursorVel = analogInput * CursorSpeed;
                return;
            }
        
            xRotation = analogInput.y * gamepadSensitivity * Time.timeScale;
            yRotation = analogInput.x * gamepadSensitivity * Time.timeScale;
        }
    }
}