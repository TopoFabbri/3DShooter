using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject firstSelectedGameObject;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private Id gameId;
    [SerializeField] private Id pauseId;
    [SerializeField] private PlayerController player;

    public bool paused;

    private EventSystem eventSystem;
    private const string MapUI = "UI";
    private const string MapWorld = "World";

    private void Start()
    {
        InputListener.Pause += OnPause;
        InputListener.Resume += OnResume;
        InputListener.Navigate += OnNavigate;
        eventSystem = EventSystem.current;
    }

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
        Cursor.lockState = CursorLockMode.None;
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
        Cursor.visible = false;
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}