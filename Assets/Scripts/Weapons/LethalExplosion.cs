using System.Collections;
using ObjectManagers;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Lethal explosion controller
    /// </summary>
    public class LethalExplosion : MonoBehaviour
    {
        [SerializeField] private float duration = 3f;
        [SerializeField] private float damage = 20f;
        [SerializeField] private float forceWave = 10f;
        [SerializeField] private string soundEvent = "PlayExplosion";

        private void OnEnable()
        {
            AkSoundEngine.PostEvent(soundEvent, gameObject);
            StartCoroutine(DestroyAfterTime(duration));
        }

        /// <summary>
        /// Destroys object on given time
        /// </summary>
        /// <param name="time">time to wait before destroying</param>
        /// <returns></returns>
        private IEnumerator DestroyAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            LethalExplosionManager.Instance.Recycle(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Rigidbody>(out var rb))
            {
                var dir = Vector3.Normalize(other.transform.position - transform.position);
                rb.AddForce(dir * forceWave);
            }

            if (other.gameObject.TryGetComponent<Stats.Stats>(out var stats))
                stats.LoseLife(damage);
        }
    }
}