using System.Collections.Generic;
using System.Linq;
using ObjectManagers;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Controller for player lethals
    /// </summary>
    public class PlayerLethalController : MonoBehaviour
    {
        [SerializeField] private GameObject defaultLethal;
        [SerializeField] private int defaultLethalQty;

        private readonly Dictionary<GameObject, int> lethalObjects = new();

        private int currentLethal;
    
        public int CurrentLethal
        {
            get => currentLethal;
            set
            {
                while (value >= lethalObjects.Count)
                    value -= lethalObjects.Count;
            
                while (value < 0)
                    value += lethalObjects.Count;
            
                currentLethal = value;
            }
        }

        public int LethalCount => lethalObjects.Count > 0 ? lethalObjects.ElementAt(CurrentLethal).Value : 0;

        private void Awake()
        {
            lethalObjects.Add(defaultLethal, defaultLethalQty);
        }

        /// <summary>
        /// Add a new lethal object to the list
        /// </summary>
        /// <param name="obj"></param>
        public void AddLethalObject(GameObject obj)
        {
            if (!lethalObjects.ContainsKey(obj))
                lethalObjects.Add(obj, 5);
            else
                lethalObjects[obj] += 5;
        }

        /// <summary>
        /// Remove a lethal object from the list
        /// </summary>
        /// <param name="obj">Object to remove</param>
        private void RemoveLethalObject(GameObject obj)
        {
            if (!lethalObjects.ContainsKey(obj))
                return;

            lethalObjects[obj]--;

            if (lethalObjects[obj] <= 0)
                RemoveAndCheck(obj);
        }

        /// <summary>
        /// Spawns current lethal at given position and rotation
        /// </summary>
        public void SpawnLethal(Vector3 pos, Quaternion rot)
        {
            if (lethalObjects.Count == 0)
                return;

            if (lethalObjects.ElementAt(CurrentLethal).Value <= 0)
                return;

            GameObject obj = lethalObjects.ElementAt(CurrentLethal).Key;

            LethalManager.Instance.Spawn(obj, pos, rot);
            RemoveLethalObject(obj);
        }

        /// <summary>
        /// Remove given object from the list and check if selected object is not on the list
        /// </summary>
        /// <param name="obj">Object to remove</param>
        private void RemoveAndCheck(GameObject obj)
        {
            lethalObjects.Remove(obj);

            if (CurrentLethal >= lethalObjects.Count)
                CurrentLethal = lethalObjects.Count - 1;
        }
    }
}