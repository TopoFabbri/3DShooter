using ObjectManagers;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Instanced bullet gun controller
    /// </summary>
    public class InstancePython : Gun
    {
        [SerializeField] private GameObject bulletPrefab;

        public override void Shoot()
        {
            if (IsReloading) return;
        
            base.Shoot();

            BulletManager.Instance.Spawn(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }
}