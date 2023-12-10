using Abstracts;
using ObjectManagers;
using UnityEngine;

namespace SOs
{
    public abstract class Pickup : SpawnableObject
    {
        [SerializeField] protected ParticleSystem pSystem;
        [SerializeField] private Transform meshHolder;

        /// <summary>
        /// Update object settings
        /// </summary>
        private void UpdateObject()
        {
            pSystem.startColor = ((PickupSettings)settings).lightColor;
            gameObject.name = ((PickupSettings)settings).prefab.name + "Pickup";
        }
        
        /// <summary>
        /// Give pickup's lethal to player
        /// </summary>
        protected abstract void PickUp();
        
        protected virtual void OnCollisionEnter(Collision other)
        {
            PickUp();
        }
        
        public override void SpawnWithSettings(SpawnableSettings settings, Vector3 pos, Quaternion rot)
        {
            this.settings = (PickupSettings)settings;
            
            UpdateObject();
            Pickup pickup = PickupManager.Instance.Spawn(gameObject, pos, rot).GetComponent<Pickup>();
            pickup.settings = (PickupSettings)settings;
            Instantiate(((PickupSettings)settings).previewModel, pickup.meshHolder);
        }
    }
}