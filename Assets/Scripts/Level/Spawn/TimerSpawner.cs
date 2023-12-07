using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level.Spawn
{
    public class TimerSpawner : Spawner
    {
        [Serializable]
        public struct SpawnableTimeSettings
        {
            public int amount;
            public float time;
        }
        
        [FormerlySerializedAs("spawnableSettings")] public List<SpawnableTimeSettings> spawnableTimeSettings;
        
        private void OnEnable()
        {
            for (int i = 0; i < spawnables.Count; i++)
            {
                StartCoroutine(SpawnLoop(i, spawnableTimeSettings[i].time));
            }
        }
        
        private IEnumerator SpawnLoop(int index, float time)
        {
            for (int i = 0; i < spawnableTimeSettings[index].amount; i++)
            {
                yield return new WaitForSeconds(time);
                Build(index);
            }
        }
    }
}
