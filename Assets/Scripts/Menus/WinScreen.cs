using System;
using Patterns;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menus
{
    /// <summary>
    /// Win screen menu controller
    /// </summary>
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private GameObject image;
        
        private EventSystem eventSystem;
    
        private void OnEnable()
        {
            eventSystem = EventSystem.current;
            InputListener.Navigate += OnNavigate;
        }

        private void OnDisable()
        {
            InputListener.Navigate -= OnNavigate;
        }

        private void Update()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        /// <summary>
        /// Call action navigate
        /// </summary>
        private void OnNavigate()
        {
            if (!eventSystem.currentSelectedGameObject)
                eventSystem.SetSelectedGameObject(image);
        }
    }
}
