using System;
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
        public int CurrentLethal
        {
            get => PlayerInventory.Instance.currentLethal;
            set
            {
                while (value >= PlayerInventory.Instance.lethals.Count)
                    value -= PlayerInventory.Instance.lethals.Count;
            
                while (value < 0)
                    value += PlayerInventory.Instance.lethals.Count;
            
                PlayerInventory.Instance.currentLethal = value;
            }
        }

        public int LethalCount => PlayerInventory.Instance.lethals.Count > 0 ? PlayerInventory.Instance.lethals.ElementAt(CurrentLethal).Value : 0;

        /// <summary>
        /// Add a new lethal object to the list
        /// </summary>
        /// <param name="obj"></param>
        public void AddLethalObject(GameObject obj)
        {
            if (!PlayerInventory.Instance.lethals.ContainsKey(obj))
                PlayerInventory.Instance.lethals.Add(obj, 5);
            else
                PlayerInventory.Instance.lethals[obj] += 5;
        }

        /// <summary>
        /// Remove a lethal object from the list
        /// </summary>
        /// <param name="obj">Object to remove</param>
        private void RemoveLethalObject(GameObject obj)
        {
            if (!PlayerInventory.Instance.lethals.ContainsKey(obj))
                return;

            PlayerInventory.Instance.lethals[obj]--;

            if (PlayerInventory.Instance.lethals[obj] <= 0)
                RemoveAndCheck(obj);
        }

        /// <summary>
        /// Spawns current lethal at given position and rotation
        /// </summary>
        public void SpawnLethal(Vector3 pos, Quaternion rot)
        {
            if (PlayerInventory.Instance.lethals.Count == 0)
                return;

            if (PlayerInventory.Instance.lethals.ElementAt(CurrentLethal).Value <= 0)
                return;

            GameObject obj = PlayerInventory.Instance.lethals.ElementAt(CurrentLethal).Key;

            LethalManager.Instance.Spawn(obj, pos, rot);
            RemoveLethalObject(obj);
        }

        /// <summary>
        /// Remove given object from the list and check if selected object is not on the list
        /// </summary>
        /// <param name="obj">Object to remove</param>
        private void RemoveAndCheck(GameObject obj)
        {
            PlayerInventory.Instance.lethals.Remove(obj);

            if (CurrentLethal >= PlayerInventory.Instance.lethals.Count)
                CurrentLethal = PlayerInventory.Instance.lethals.Count - 1;
        }
    }
}