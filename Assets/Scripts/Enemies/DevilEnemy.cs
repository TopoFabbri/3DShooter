using System;
using Abstracts;
using ObjectManagers;
using SOs;
using UnityEngine;

namespace Enemies
{
    public class DevilEnemy : Enemy
    {
        private float nextFireTime;
        private int fireCount;

        public static event Action<GameObject> DevilDestroyed;
    
        /// <summary>
        /// Gameplay-only update
        /// </summary>
        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (Time.time < nextFireTime) return;
        
            if (Vector3.Distance(transform.position, ((EnemySettings)settings).target.position) > ((DevilSettings)settings).fireDis)
            {
                obstacleEvasion.CheckAndEvade();
                rb.AddForce(((EnemySettings)settings).speed * transform.forward, ForceMode.Acceleration);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, ((EnemySettings)settings).maxSpeed);
            }
            else
            {
                Shoot();
            }
        }

        /// <summary>
        /// Instantiate a fireball
        /// </summary>
        private void Shoot()
        {
            fireCount++;

            nextFireTime = Time.time + (fireCount >= 3 ? ((DevilSettings)settings).longCooldown : ((DevilSettings)settings).cooldown);

            if (fireCount >= 3)
                fireCount = 0;

            var trans = transform;
        
            FireballManager.Instance.Spawn(((DevilSettings)settings).fireBall, trans.position + trans.forward, trans.rotation);
        }
    
        protected override void DieHandler()
        {
            DevilDestroyed?.Invoke(gameObject);
            EnemyManager.Instance.Recycle(gameObject);
        }
    }
}