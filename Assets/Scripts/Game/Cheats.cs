using System;
using UnityEngine;

namespace Game
{
    public class Cheats : MonoBehaviour
    {
        public static event Action NextLevel;
        public static event Action GodMode;
        public static event Action Flash;
        public static event Action Nuke;

        private void OnNextLevel()
        {
            NextLevel?.Invoke();
        }

        private void OnGodMode()
        {
            GodMode?.Invoke();
        }

        private void OnFlash()
        {
            Flash?.Invoke();
        }

        private void OnNuke()
        {
            Nuke?.Invoke();
        }
    }
}
