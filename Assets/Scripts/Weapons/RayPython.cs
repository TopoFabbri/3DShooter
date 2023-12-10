using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Raycast bullet weapon controller
    /// </summary>
    public class RayPython : Gun
    {
        [SerializeField] private float strength = 10f;
        [SerializeField] private float damage = 50f;
    
        public override void Shoot()
        {
            if (IsReloading) return;
        
            var ray = new Ray(bulletSpawnPoint.position, bulletSpawnPoint.forward);

            base.Shoot();

            if (!Physics.Raycast(ray, out var hit, 100f, LayerMask.GetMask("Enemy", "Walls", "Player", "Projectile"))) return;
        
            weaponVFX.PlayHitExplosion(hit.point);

            if (hit.transform.gameObject.GetComponent<Stats.Stats>())
                hit.transform.gameObject.GetComponent<Stats.Stats>().LoseLife(damage);

            if (hit.transform.gameObject.GetComponent<Rigidbody>())
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce((hit.transform.position - hit.point).normalized * strength, ForceMode.Impulse);
        }
    }
}