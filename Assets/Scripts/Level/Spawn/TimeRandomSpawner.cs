using System.Collections;
using GameStats;
using UnityEngine;

namespace Level.Spawn
{
    /// <summary>
    /// Spawns random objects by time
    /// </summary>
    public class TimeRandomSpawner : Spawner
    {
        [SerializeField] private float time;

        private bool ended;
        
        private int Random => UnityEngine.Random.Range(0, spawnables.Count);

        private void OnEnable()
        {
            StartCoroutine(SpawnLoop(time));
        }

        /// <summary>
        /// Wait for time, then spawn random object
        /// </summary>
        /// <param name="time">Delay between spawning</param>
        /// <returns></returns>
        private IEnumerator SpawnLoop(float time)
        {
            while (!ended)
            {
                float waitEndTime = GameTimeCounter.Instance.GameTime + time;
                    
                yield return new WaitUntil(() => GameTimeCounter.Instance.GameTime >= waitEndTime);
                Spawn(Random);
            }
        }
    }
}
