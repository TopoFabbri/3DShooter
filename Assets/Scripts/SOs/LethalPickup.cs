using Abstracts;
using Character;
using Lethals;
using ObjectManagers;
using UnityEngine;

namespace SOs
{
    /// <summary>
    /// Spawnable pickup
    /// </summary>
    public class LethalPickup : SpawnableObject
    {
        [SerializeField] private ParticleSystem pSystem;
        [SerializeField] private MeshFilter lethalMesh;

        /// <summary>
        /// Update object settings
        /// </summary>
        private void UpdateObject()
        {
            pSystem.startColor = ((PickupSettings)settings).lightColor;
            lethalMesh.mesh = ((PickupSettings)settings).mesh;
            gameObject.name = ((PickupSettings)settings).lethalPrefab.name + "Pickup";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out PlayerLethalController playerLethalController)) return;
            
            PickUp(playerLethalController);
        }
        
        /// <summary>
        /// Give pickup's lethal to player
        /// </summary>
        /// <param name="playerLethalController">Player's lethal manager</param>
        private void PickUp(PlayerLethalController playerLethalController)
        {
            playerLethalController.AddLethalObject(((PickupSettings)settings).lethalPrefab);
            PickupManager.Instance.Recycle(gameObject);
        }
        
        public override void SpawnWithSettings(SpawnableSettings settings, Vector3 pos, Quaternion rot)
        {
            this.settings = (PickupSettings)settings;
            
            UpdateObject();
            PickupManager.Instance.Spawn(gameObject, pos, rot).GetComponent<LethalPickup>().settings = (PickupSettings)settings;
        }
    }
}
