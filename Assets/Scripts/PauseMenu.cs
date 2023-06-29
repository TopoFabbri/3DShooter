using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject firstSelectedGameObject;
    [SerializeField] private PlayerInput playerInput;

    public bool paused = false;

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

    public void OnPause()
    {
        paused = true;
        playerInput.SwitchCurrentActionMap(MapUI);
        eventSystem.SetSelectedGameObject(firstSelectedGameObject);
        pauseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnResume()
    {
        paused = false;
        playerInput.SwitchCurrentActionMap(MapWorld);
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}