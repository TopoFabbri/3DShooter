using Level;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Menus
{
    /// <summary>
    /// Main menu controller
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelection;
        private EventSystem eventSystem;
    
        private void Start()
        {
            eventSystem = EventSystem.current;
            Cursor.lockState = CursorLockMode.Confined;
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
        public void LoadScene(SceneId scene)
        {
            SceneLoader.Instance.LoadScene(scene);
        }

        /// <summary>
        /// Exit application
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
    }
}
