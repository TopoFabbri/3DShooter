using System;
using Character;
using Level;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SceneId menuId;
        
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance != null) return instance;

                instance = FindObjectOfType<GameManager>();

                if (instance != null) return instance;

                GameObject singletonObject = new();
                instance = singletonObject.AddComponent<GameManager>();
                singletonObject.name = typeof(GameManager) + " (Singleton)";

                DontDestroyOnLoad(singletonObject);
                return instance;
            }
        }

        private void OnEnable()
        {
            SceneLoader.OnSceneLoaded += OnSceneLoadedHandler;
        }

        private void OnDisable()
        {
            SceneLoader.OnSceneLoaded -= OnSceneLoadedHandler;
        }

        private void Awake()
        {
            if (instance != null && instance != this)
                DestroyImmediate(gameObject);
            else
                instance = this;
        }

        private void OnSceneLoadedHandler(SceneId loadedSceneId)
        {
            if (loadedSceneId == menuId)
                PlayerInventory.Instance.Reset();
        }
        
        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
    }
}