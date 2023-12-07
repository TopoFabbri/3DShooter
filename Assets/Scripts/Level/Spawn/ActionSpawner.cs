using System;
using UnityEngine;

namespace Level.Spawn
{
    public class ActionSpawner : Spawner
    {
        [SerializeField] private Action<int> action;
        
        public int Range => spawnables.Count;

        private void OnEnable()
        {
            action += Build;
        }
        
        private void OnDisable()
        {
            action -= Build;
        }
    }
}