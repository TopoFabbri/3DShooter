using System;
using System.Collections.Generic;
using Abstracts;
using UnityEngine;

namespace Level.Spawn
{
    /// <summary>
    /// Class to spawn objects with settings
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [Serializable]
        public struct Spawnable
        {
            public SpawnableObject recyclable;
            public SpawnableSettings settings;
        }
    
        [SerializeField] protected List<Spawnable> spawnables;

        /// <summary>
        /// Spawn object by index
        /// </summary>
        /// <param name="index">Position in list of object to spawn</param>
        protected virtual void Spawn(int index)
        {
            Transform trans = transform;
            
            spawnables[index].recyclable
                .SpawnWithSettings(spawnables[index].settings, trans.position, trans.rotation);
        }
    }
}
