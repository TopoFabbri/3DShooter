using UnityEngine;

namespace Abstracts
{
    public abstract class SpawnableObject : MonoBehaviour, ISpawnable
    {
        protected SpawnableSettings settings;
        
        public abstract void SpawnWithSettings(SpawnableSettings settings, Vector3 pos, Quaternion rot);
    }
}
