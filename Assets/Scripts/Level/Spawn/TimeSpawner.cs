using System.Collections;
using System.Collections.Generic;
using ObjectManagers;
using UnityEngine;

namespace Level.Spawn
{
    public class TimeSpawner : Spawner
    {
        [SerializeField] private List<float> delays;
                
        private void OnEnable()
        {
            Recyclable.Init();
            
            for (int i = 0; i < spawnables.Count; i++)
                StartCoroutine(SpawnLoop(i, delays[i]));
        }

        /// <summary>
        /// Spawns object waits given time and spawns next
        /// </summary>
        /// <param name="index">Place in list of obj to spawn</param>
        /// <param name="time">Interval between spawns</param>
        /// <returns></returns>
        private IEnumerator SpawnLoop(int index, float time)
        {
            for (int i = 0; i < spawnables[index].amount; i++)
            {
                yield return new WaitForSeconds(time);
                Spawn(index);
            }
        }
    }
}
