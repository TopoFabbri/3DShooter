using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    /// <summary>
    /// Load scenes by SceneId
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        [Serializable] private struct SceneWithBuildIndex
        {
            public SceneId scene;
            public int buildIndex;
        }
    
        [SerializeField] private SceneWithBuildIndex[] scenesIndex;

        /// <summary>
        /// Load scene from scene id
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(SceneId scene)
        {
            foreach (var sceneWithIndex in scenesIndex)
            {
                if (sceneWithIndex.scene != scene) continue;
            
                SceneManager.LoadScene(sceneWithIndex.buildIndex);
                break;
            }
        }
    }
}
