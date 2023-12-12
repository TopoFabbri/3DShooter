using System.Collections.Generic;
using Abstracts;
using ObjectManagers;
using UnityEngine;
using Weapons;

namespace SOs
{
    /// <summary>
    /// Spawnable weapon pickup
    /// </summary>
    public class WeaponPickup : Pickup
    {
        public static List<WeaponPickup> weaponsInMap = new(); 
        
        private IGunHolder gunHolder;
        
        protected override void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out gunHolder)) return;
            
            base.OnCollisionEnter(other);
        }
        
        private void OnCollisionExit(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out gunHolder)) return;
            gunHolder = null;
        }
        
        protected override void PickUp()
        {
            if (gunHolder == null) return;

            GameObject weapon = Instantiate(((PickupSettings)settings).prefab);
            
            if (weapon.TryGetComponent(out Gun gun))
                gunHolder.AddGun(gun);
            
            PickupManager.Instance.Recycle(gameObject);
        }

        public override SpawnableObject SpawnWithSettings(SpawnableSettings settings, Vector3 pos, Quaternion rot)
        {
            WeaponPickup pickup = (WeaponPickup)base.SpawnWithSettings(settings, pos, rot);
            
            weaponsInMap.Add(pickup);
            return pickup;
        }
    }
}