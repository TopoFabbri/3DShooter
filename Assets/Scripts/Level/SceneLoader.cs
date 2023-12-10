using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class SceneLoader : MonoBehaviour
    {
        [Serializable] private struct SceneWithBuildIndex
        {
            public SceneId scene;
            public int buildIndex;
        }

        [SerializeField] private SceneWithBuildIndex[] scenesIndex;

        private SceneId currentScene;
        
        public SceneId CurrentScene => currentScene;
        
        public static event Action<SceneId> OnSceneLoaded;

        private static SceneLoader instance;

        public static SceneLoader Instance
        {
            get
            {
                if (instance != null) return instance;

                instance = FindObjectOfType<SceneLoader>();

                if (instance != null) return instance;

                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<SceneLoader>();
                singletonObject.name = typeof(SceneLoader) + " (Singleton)";

                DontDestroyOnLoad(singletonObject);
                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
                DestroyImmediate(gameObject);
            else
                instance = this;
        }

        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }

        public void LoadScene(SceneId scene)
        {
            foreach (var sceneWithIndex in scenesIndex)
            {
                if (sceneWithIndex.scene != scene) continue;

                OnSceneLoaded?.Invoke(scene);
                currentScene = scene;
                
                SceneManager.LoadScene(sceneWithIndex.buildIndex);
                break;
            }
        }
    }
}