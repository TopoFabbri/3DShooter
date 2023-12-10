using ObjectManagers;
using Patterns.SM;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Instanced bullet gun controller
    /// </summary>
    public class InstancePython : Gun
    {
        [SerializeField] private GameObject bulletPrefab;

        private void OnEnable()
        {
            if (!stateMachine)
                stateMachine = GameObject.Find("StateMachine").GetComponent<StateMachine>();
                
            stateMachine.Subscribe(stateId, OnUpdate);
        }

        private void OnDisable()
        {
            stateMachine.UnSubscribe(stateId, OnUpdate);
        }

        /// <summary>
        /// Gameplay-only update
        /// </summary>
        private void OnUpdate()
        {
            CheckReload();
        }

        public override void Shoot()
        {
            if (IsReloading) return;
        
            base.Shoot();

            BulletManager.Instance.Spawn(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }
}