using System;
using System.Collections;
using Character;
using Game;
using JetBrains.Annotations;
using Level.Spawn;
using ObjectManagers;
using UnityEngine;

namespace Level
{
    /// <summary>
    /// Manage all actions in scene
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager instance;
    
        [SerializeField] private SceneId menu;
        [SerializeField] private SceneId credits;
        [SerializeField] private GameObject winCanvas;
        [SerializeField] private GameObject loseScreen;
        [SerializeField] private TimerSpawner[] spawners;
        [SerializeField] private SceneId nextLevelId;
        
        public static LevelManager Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<LevelManager>();
            
                if (!instance)
                    instance = new GameObject("LevelManager").AddComponent<LevelManager>();
            
                return instance;
            }
        }

        private int spawnedEnemies;
        private int enemyCount;

        private void Awake()
        {
            if (instance != null && instance != this)
                DestroyImmediate(gameObject);
            else
                instance = this;
        }

        private void OnEnable()
        {
            Cheats.NextLevel += OnNextLevelHandler;
        }

        private void OnDisable()
        {
            Cheats.NextLevel -= OnNextLevelHandler;
        }
        
        private void Update()
        {
            CheckWinCondition();
        }

        /// <summary>
        /// Check if level was won and show win screen
        /// </summary>
        private void CheckWinCondition()
        {
            bool spawnersEnded = true;
            
            foreach (TimerSpawner spawner in spawners)
            {
                if (!spawner.Ended)
                    spawnersEnded = false;
            }
            
            if (EnemyManager.EnemiesAlive <= 0 && spawnersEnded)
                ShowWinScreen();
        }

        /// <summary>
        /// Activate win canvas
        /// </summary>
        private void ShowWinScreen()
        {
            winCanvas.SetActive(true);
        }

        /// <summary>
        /// Start event player lose
        /// </summary>
        public void Lose()
        {
            StartCoroutine(WaitAndChangeScene(1, credits));
        }

        /// <summary>
        /// Change scene on given time
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="sceneId"></param>
        /// <returns></returns>
        private IEnumerator WaitAndChangeScene(int seconds, [NotNull] SceneId sceneId)
        {
            if (!sceneId) throw new ArgumentNullException(nameof(sceneId));
        
            loseScreen.SetActive(true);
        
            yield return new WaitForSeconds(seconds);
        
            SceneLoader.Instance.LoadScene(sceneId);
        }

        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
        
        /// <summary>
        /// Load next level
        /// </summary>
        public void OnNextLevelHandler()
        {
            SceneLoader.Instance.LoadScene(nextLevelId);
        }
    }
}