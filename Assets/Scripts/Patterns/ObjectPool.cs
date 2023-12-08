using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    /// <summary>
    /// Helper class for pooling GameObjects
    /// </summary>
    public class ObjectPool
    {
        private readonly Queue<GameObject> pool = new();

        /// <summary>
        /// Attempts to get an object from the pool
        /// </summary>
        /// <returns>The object or null if the pool is empty</returns>
        public GameObject Get()
        {
            GameObject obj = pool.Count == 0 ? null : pool.Dequeue();

            return obj;
        }

        /// <summary>
        /// Add an object to the pool
        /// </summary>
        /// <param name="obj">Object to add</param>
        public void Release(GameObject obj)
        {
            pool.Enqueue(obj);
            obj.SetActive(false);
        }
    }
}