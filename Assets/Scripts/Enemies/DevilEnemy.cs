using System;
using ObjectManagers;
using SOs;
using UnityEngine;

namespace Enemies
{
    public class DevilEnemy : Enemy
    {
        private float nextFireTime;
        private int fireCount;

        /// <summary>
        /// Gameplay-only update
        /// </summary>
        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (Time.time < nextFireTime) return;

            if (Vector3.Distance(transform.position, ((EnemySettings)settings).target.position) >
                ((DevilSettings)settings).fireDis)
            {
                obstacleEvasion.CheckAndEvade();
                Move(transform.forward);
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

            nextFireTime = Time.time +
                           (fireCount >= 3
                               ? ((DevilSettings)settings).longCooldown
                               : ((DevilSettings)settings).cooldown);

            if (fireCount >= 3)
                fireCount = 0;

            var trans = transform;

            FireballManager.Instance.Spawn(((DevilSettings)settings).fireBall, trans.position + trans.forward,
                trans.rotation);
        }
    }
}