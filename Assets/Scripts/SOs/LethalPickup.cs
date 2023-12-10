using Character;
using ObjectManagers;
using UnityEngine;

namespace SOs
{
    /// <summary>
    /// Spawnable lethal pickup
    /// </summary>
    public class LethalPickup : Pickup
    {
        private PlayerLethalController playerLethalController;

        protected override void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out playerLethalController)) return;
            
            base.OnCollisionEnter(other);
        }
        
        private void OnCollisionExit(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out playerLethalController)) return;
                playerLethalController = null;
        }

        protected override void PickUp()
        {
            if (!playerLethalController) return;
            
            playerLethalController.AddLethalObject(((PickupSettings)settings).prefab);
            PickupManager.Instance.Recycle(gameObject);
        }
    }
}
