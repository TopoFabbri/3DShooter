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
        
        private void Start()
        {
            if (!settings) return;
            
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
