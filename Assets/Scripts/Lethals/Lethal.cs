using ObjectManagers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lethals
{
    public class Lethal : MonoBehaviour
    {
        [FormerlySerializedAs("barrelExplosion")] [SerializeField] protected GameObject explosionPrefab;

        protected Rigidbody rb;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        protected virtual void OnDisable()
        {
            rb.velocity = Vector3.zero;
        }

        /// <summary>
        /// Instantiate explosion
        /// </summary>
        protected virtual void Explode()
        {
            var trans = transform;

            LethalExplosionManager.Instance.Spawn(explosionPrefab, trans.position, trans.rotation);
            LethalManager.Instance.Recycle(gameObject);
        }
    }
}