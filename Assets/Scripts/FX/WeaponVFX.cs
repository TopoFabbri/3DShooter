using ObjectManagers;
using UnityEngine;

namespace FX
{
    /// <summary>
    /// Manages weapon VFX
    /// </summary>
    public class WeaponVFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particleSys;
        [SerializeField] private GameObject particleSysPrefab;
        [SerializeField] protected Animation anim;

        /// <summary>
        /// Play shooting effects
        /// </summary>
        public void Shoot()
        {
            anim.Play();
            particleSys.Play();
        }

        /// <summary>
        /// Play explosion on hit point
        /// </summary>
        /// <param name="pos"></param>
        public void PlayHitExplosion(Vector3 pos)
        {
            ExplosionManager.Instance.Spawn(particleSysPrefab, pos, Quaternion.identity);
        }
    }
}
