using UnityEngine;

namespace Abstracts
{
    /// <summary>
    /// Interface for objects that can be spawned
    /// </summary>
    public interface ISpawnable
    {
        /// <summary>
        /// Spawn object with given settings
        /// </summary>
        /// <param name="settings">Spawnable's settings</param>
        /// <param name="pos">Spawn position</param>
        /// <param name="rot">Spawnable rotation</param>
        SpawnableObject SpawnWithSettings(SpawnableSettings settings, Vector3 pos, Quaternion rot);
    }
}
