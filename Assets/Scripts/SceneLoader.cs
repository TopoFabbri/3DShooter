using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Serializable] private struct SceneWithBuildIndex
    {
        public SceneId scene;
        public int buildIndex;
    }
    
    [SerializeField] private SceneWithBuildIndex[] scenesIndex;

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
