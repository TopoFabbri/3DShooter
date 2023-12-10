using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Character
{
    public class PlayerInventory
    {
        private static PlayerInventory instance;
        
        public readonly Dictionary<GameObject, int> lethals = new();
        public int currentLethal = 0;

        public int bullets;

        public static PlayerInventory Instance
        {
            get
            {
                if (instance != null) return instance;
                
                instance = new PlayerInventory();
                instance.Reset();

                return instance;
            }
        }

        public static string Text
        {
            get
            {
                string text = "";
                
                for (int i = 0; i < Instance.lethals.Count; i++)
                {
                    if (i == Instance.currentLethal)
                        text += ">> ";
                    
                    text += Instance.lethals.ElementAt(i).Key.name + "s: " + Instance.lethals.ElementAt(i).Value + "\n";
                }
                
                return text;
            }
        }

        /// <summary>
        /// Reset inventory
        /// </summary>
        public void Reset()
        {
            lethals.Clear();
        }
    }
}