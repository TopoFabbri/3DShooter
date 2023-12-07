using Abstracts;
using Character;
using ObjectManagers;
using UnityEngine;

namespace Lethals
{
    public class LethalPickup : SpawnableObject
    {
        [SerializeField] private ParticleSystem pSystem;
        [SerializeField] private MeshFilter lethalMesh;

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
