using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject firstSelection;
    private EventSystem eventSystem;
    
    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        if (!eventSystem.currentSelectedGameObject)
            eventSystem.SetSelectedGameObject(firstSelection);
    }

    /// <summary>
    /// Load a scene from number
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// Exit application
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
