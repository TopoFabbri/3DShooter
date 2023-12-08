using System.Collections;
using Menus;
using UnityEngine;

namespace GameStats
{
    /// <summary>
    /// Custom time counter
    /// </summary>
    public class GameTimeCounter : MonoBehaviour
    {
        [SerializeField] private PauseMenu pauseMenu;
    
        private const int SecondsToTimeUnit = 1;
        public int GameTime { get; private set; }

        private void Awake()
        {
            StartCoroutine(CountGameTime());
        }

        /// <summary>
        /// Custom time counter loop
        /// </summary>
        /// <returns></returns>
        private IEnumerator CountGameTime()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(SecondsToTimeUnit);
                GameTime++;

                if (pauseMenu.paused)
                    yield return new WaitUntil(() => !pauseMenu.paused);
            }
        }
    }
}
