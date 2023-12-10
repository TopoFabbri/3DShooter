using Character;
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
        private PlayerController playerController;
        
        protected override void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out playerController)) return;
            
            base.OnCollisionEnter(other);
        }
        
        private void OnCollisionExit(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out playerController)) return;
            playerController = null;
        }
        
        protected override void PickUp()
        {
            if (!playerController) return;

            GameObject weapon = Instantiate(((PickupSettings)settings).prefab);
            
            if (weapon.TryGetComponent(out Gun gun))
                playerController.AddGun(gun);
            
            PickupManager.Instance.Recycle(gameObject);
        }
    }
}