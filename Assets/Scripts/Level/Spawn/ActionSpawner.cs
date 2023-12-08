using System;
using UnityEngine;

namespace Level.Spawn
{
    /// <summary>
    /// Class to spawn objects by an action
    /// </summary>
    public class ActionSpawner : Spawner
    {
        [SerializeField] private Action<int> action;
        
        public int Range => spawnables.Count;

        private void OnEnable()
        {
            action += Spawn;
        }
        
        private void OnDisable()
        {
            action -= Spawn;
        }
    }
}