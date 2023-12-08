using Character;
using Patterns;
using Patterns.SM;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Menus
{
    /// <summary>
    /// Pause menu controller
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject firstSelectedGameObject;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private Id gameId;
        [SerializeField] private Id pauseId;
        [SerializeField] private PlayerController player;
        [SerializeField] private float cursorSpeed = 20f;

        public bool paused;

        private EventSystem eventSystem;
        private const string MapUI = "UI";
        private const string MapWorld = "World";
        private Vector2 cursorVel;

        private void Start()
        {
            eventSystem = EventSystem.current;
        }

        private void OnEnable()
        {
            stateMachine.Subscribe(pauseId, OnUpdate);
            InputListener.Pause += OnPause;
            InputListener.Resume += OnResume;
            InputListener.Navigate += OnNavigate;
            InputListener.MoveCursor += OnCursor;
        }

        private void OnDisable()
        {
            InputListener.Pause -= OnPause;
            InputListener.Resume -= OnResume;
            InputListener.Navigate -= OnNavigate;
            InputListener.MoveCursor -= OnCursor;
        }

        private void OnUpdate()
        {
            Mouse.current.WarpCursorPosition((Vector2)Input.mousePosition + cursorVel);
        }

        /// <summary>
        /// Call action navigate
        /// </summary>
        private void OnNavigate()
        {
            if (!eventSystem.currentSelectedGameObject)
                eventSystem.SetSelectedGameObject(firstSelectedGameObject);
        }

        /// <summary>
        /// Call pause action
        /// </summary>
        public void OnPause()
        {
            paused = true;
            stateMachine.ChangeState(pauseId);
            playerInput.SwitchCurrentActionMap(MapUI);
            eventSystem.SetSelectedGameObject(firstSelectedGameObject);
            pauseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            player.StopMovement();
        }

        /// <summary>
        /// Call action resume
        /// </summary>
        public void OnResume()
        {
            paused = false;
            stateMachine.ChangeState(gameId);
            playerInput.SwitchCurrentActionMap(MapWorld);
            pauseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        /// <summary>
        /// Call action move cursor
        /// </summary>
        /// <param name="input"></param>
        private void OnCursor(InputValue input)
        {
            cursorVel = input.Get<Vector2>() * cursorSpeed;
        }
    }
}