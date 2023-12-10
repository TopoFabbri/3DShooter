using System;
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
        private static GameTimeCounter instance;
        
        [SerializeField] private PauseMenu pauseMenu;
    
        private const int SecondsToTimeUnit = 1;

        public static GameTimeCounter Instance
        {
            get
            {
                if (instance != null) return instance;

                instance = FindObjectOfType<GameTimeCounter>();

                if (instance != null) return instance;

                GameObject singletonObject = new();
                instance = singletonObject.AddComponent<GameTimeCounter>();
                singletonObject.name = typeof(GameTimeCounter) + " (Singleton)";

                DontDestroyOnLoad(singletonObject);
                return instance;
            }
        }

        public int GameTime { get; private set; }

        private void Awake()
        {
            if (instance != null && instance != this)
                DestroyImmediate(gameObject);
            else
                instance = this;

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

        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
    }
}
