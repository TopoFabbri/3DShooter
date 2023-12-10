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

        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out playerLethalController)) return;
            
            base.OnTriggerEnter(other);
        }
        
        private void OnTriggerExit(Collider other)
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
