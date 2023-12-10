using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Raycast bullet weapon controller
    /// </summary>
    public class RayPython : Gun
    {
        [SerializeField] private float strength = 10f;
        [SerializeField] private Transform character;
        [SerializeField] private float damage = 50f;

        private void OnEnable()
        {
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
        
            var ray = new Ray(bulletSpawnPoint.position, bulletSpawnPoint.forward);

            base.Shoot();

            if (!Physics.Raycast(ray, out var hit)) return;
        
            weaponVFX.PlayHitExplosion(hit.point);

            if (hit.transform.gameObject.GetComponent<Stats.Stats>())
                hit.transform.gameObject.GetComponent<Stats.Stats>().LoseLife(damage);

            if (hit.transform.gameObject.GetComponent<Rigidbody>())
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce((hit.transform.position - hit.point).normalized * strength, ForceMode.Impulse);
        }
    }
}