using System;
using System.Collections;
using Character;
using GameStats;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    /// <summary>
    /// Control drone actions
    /// </summary>
    public class DroneEnemy : Enemy
    {
        [SerializeField] private LineRenderer lineRenderer;

        private DroneSettings Settings => settings as DroneSettings;

        private bool strafing;
        private bool movingRight;
        private bool shooting;
        private bool cooldownFinished;

        protected override void OnUpdate()
        {
            base.OnUpdate();

            lineRenderer.SetPosition(0, transform.position);

            if (Settings.disToPlayer < Vector3.Distance(transform.position, Settings.target.position))
            {
                transform.LookAt(Settings.target);
                obstacleEvasion.CheckAndEvade();
                Move(transform.forward);
            }
            else
            {
                ManageShoot();
            }

            if (!strafing)
                StartCoroutine(StartStrafe());
            else if (shooting && !cooldownFinished)
                Move(movingRight ? transform.right : -transform.right);

            if (transform.position.y < Settings.flyHeight)
                Move(transform.up);
        }

        /// <summary>
        /// Check if can shoot and start sequence
        /// </summary>
        private void ManageShoot()
        {
            if (shooting)
                return;

            StartCoroutine(PrepareAndFire());
        }

        /// <summary>
        /// Sets prepare fire, then fires
        /// </summary>
        /// <returns></returns>
        private IEnumerator PrepareAndFire()
        {
            shooting = true;

            rb.velocity = Vector3.zero;

            float shotTime = GameTimeCounter.Instance.GameTime + Settings.fireTime;

            lineRenderer.SetPosition(1, Settings.target.position);
            lineRenderer.startColor = new Color(1f, 0f, 0f, .2f);
            lineRenderer.endColor = new Color(1f, 0f, 0f, .2f);

            yield return new WaitUntil(() => GameTimeCounter.Instance.GameTime > shotTime);

            Fire();
        }

        /// <summary>
        /// Shoot paralyzer ray
        /// </summary>
        private void Fire()
        {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;

            Vector3 rayStart = lineRenderer.GetPosition(0);
            Vector3 rayEnd = lineRenderer.GetPosition(1);
            Vector3 rayDir = (rayEnd - rayStart).normalized;

            float rayDis = Vector3.Distance(rayStart, rayEnd);

            if (Physics.Raycast(rayStart, rayDir, out RaycastHit hit, rayDis,
                    LayerMask.GetMask("Walls", "Player")))
            {
                lineRenderer.SetPosition(1, hit.point);
                if (hit.transform.TryGetComponent(out PlayerController character))
                    character.Paralyze(Settings.paralyzeTime);
            }

            StartCoroutine(StopRay());
        }

        /// <summary>
        /// Start strafing
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartStrafe()
        {
            strafing = true;
            movingRight = !movingRight;

            yield return new WaitForSeconds(Random.Range(Settings.minMoveTime, Settings.maxMoveTime));
            strafing = false;
        }

        private IEnumerator StopRay()
        {
            float endTime = GameTimeCounter.Instance.GameTime + Settings.showRayTime;
            float cooldownTime = GameTimeCounter.Instance.GameTime + Settings.fireCooldown;

            yield return new WaitUntil(() => GameTimeCounter.Instance.GameTime > endTime);
            lineRenderer.startColor = Color.clear;
            lineRenderer.endColor = Color.clear;

            cooldownFinished = false;

            yield return new WaitUntil(() => GameTimeCounter.Instance.GameTime > cooldownTime);
            shooting = false;

            cooldownFinished = true;
        }
    }
}