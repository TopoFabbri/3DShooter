using ObjectManagers;
using UnityEngine;

namespace Lethals
{
    /// <summary>
    /// Mine lethal controller
    /// </summary>
    public class Mine : Lethal
    {
        [SerializeField] private string characterTag = "Character";

        /// <summary>
        /// Start action explode
        /// </summary>
        protected override void Explode()
        {
            var trans = transform;
        
            LethalExplosionManager.Instance.Spawn(explosionPrefab, trans.position, trans.rotation);
            Destroy(gameObject);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(characterTag))
                Explode();
        }
    }
}
