using System;
using ObjectManagers;
using SOs;
using UnityEngine;

namespace Enemies
{
    public class NormalEnemy : Enemy
    {
        private float cooldown;
        private bool isInCooldown;
    
        /// <summary>
        /// Gameplay-only update
        /// </summary>
        protected override void OnUpdate()
        {
            base.OnUpdate();

            obstacleEvasion.CheckAndEvade();
            rb.AddForce(((EnemySettings)settings).speed * transform.forward, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, ((EnemySettings)settings).maxSpeed);
        }

        private void OnCollisionStay(Collision other)
        {
            if (!(Time.time > cooldown && other.gameObject.CompareTag(((NormalSettings)settings).characterTag))) return;
        
            if (other.gameObject.TryGetComponent<Stats.Stats>(out var otherStats))
                otherStats.LoseLife(((NormalSettings)settings).damage);
            cooldown = Time.time + ((NormalSettings)settings).damageTime;
        }
    }
}