using System.Collections.Generic;
using Lethals;
using UnityEngine;

namespace Level.Spawn
{
    public class PickupSpawner : TimeSpawner
    {
        [SerializeField] private List<LethalPickupSettings> settingsList = new();

        protected override void Spawn(int index)
        {
            base.Spawn(index);

            if (spawned.TryGetComponent(out LethalPickup lethal))
                lethal.Settings = settingsList[index];
        }
    }
}
