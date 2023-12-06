using System;
using System.Collections.Generic;
using ObjectManagers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level.Spawn
{
    public abstract class Spawner : MonoBehaviour
    {
        [Serializable]
        protected struct Spawnable
        {
            public Recyclable recyclable;
            public int amount;
        }
        
        [FormerlySerializedAs("recyclables")]
        [Header("Recyclables:")]
        [SerializeField] protected List<Spawnable> spawnables;

        protected GameObject spawned;
        
        protected virtual void Spawn(int index)
        {
            spawned = spawnables[index].recyclable.Get(transform.position, transform.rotation);
        }
    }
}
