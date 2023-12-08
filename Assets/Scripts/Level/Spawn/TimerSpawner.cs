using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level.Spawn
{
    /// <summary>
    /// Class to spawn objects by time
    /// </summary>
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
        
        /// <summary>
        /// Wait for time, then spawn object
        /// </summary>
        /// <param name="index">Number on the list of the object to spawn</param>
        /// <param name="time">Delay between spawning</param>
        /// <returns></returns>
        private IEnumerator SpawnLoop(int index, float time)
        {
            for (int i = 0; i < spawnableTimeSettings[index].amount; i++)
            {
                yield return new WaitForSeconds(time);
                Spawn(index);
            }
        }
    }
}
