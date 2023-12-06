using Character;
using ObjectManagers;
using UnityEngine;

namespace Lethals
{
    public class LethalPickup : MonoBehaviour
    {
        [SerializeField] private LethalPickupSettings settings;
        [SerializeField] private ParticleSystem pSystem;
        [SerializeField] private MeshFilter lethalMesh;

        public LethalPickupSettings Settings
        {
            set
            {
                settings = value; 
                UpdateObject();
            }
        }
        
        private void Start()
        {
            UpdateObject();
        }

        private void UpdateObject()
        {
            pSystem.startColor = settings.lightColor;
            lethalMesh.mesh = settings.mesh;
            gameObject.name = settings.lethalPrefab.name + "Pickup";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out PlayerLethalController playerLethalController)) return;
            
            PickUp(playerLethalController);
        }
        
        private void PickUp(PlayerLethalController playerLethalController)
        {
            playerLethalController.AddLethalObject(settings.lethalPrefab);
            PickupManager.Instance.Recycle(gameObject);
        }
    }
}
