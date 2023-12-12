using UnityEngine;

namespace Abstracts
{
    /// <summary>
    /// Object that can be spawned with Spawner
    /// </summary>
    public abstract class SpawnableObject : MonoBehaviour, ISpawnable
    {
        protected SpawnableSettings settings;
        
        public abstract SpawnableObject SpawnWithSettings(SpawnableSettings settings, Vector3 pos, Quaternion rot);
    }
}
