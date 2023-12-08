using UnityEngine;

namespace Level
{
    /// <summary>
    /// Scene id container
    /// </summary>
    [CreateAssetMenu(fileName = "SceneId_", menuName = "Data/SceneId")]
    public class SceneId : ScriptableObject
    {
        public string id => name;
    }
}
