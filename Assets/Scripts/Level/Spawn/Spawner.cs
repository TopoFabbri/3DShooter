using System;
using System.Collections.Generic;
using Abstracts;
using UnityEngine;

namespace Level.Spawn
{
    public class Spawner : MonoBehaviour
    {
        [Serializable]
        public struct Spawnable
        {
            public SpawnableObject recyclable;
            public SpawnableSettings settings;
        }
    
        [SerializeField] protected List<Spawnable> spawnables;

        protected void Build(int index)
        {
            Transform trans = transform;
            
            spawnables[index].recyclable
                .SpawnWithSettings(spawnables[index].settings, trans.position, trans.rotation);
        }
    }
}
