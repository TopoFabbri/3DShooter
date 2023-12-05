using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BarrelExplosion : MonoBehaviour
{
    [SerializeField] private float duration = 3f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float forceWave = 10f;
    [SerializeField] private ParticleSystem ps;

    private void Awake()
    {
        StartCoroutine(DestroyAfterTime(duration));
    }

    private void OnEnable()
    {
        ps.Play();
        StartCoroutine(DestroyAfterTime(duration));
    }

    /// <summary>
    /// Destroys object on given time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Rigidbody>(out var rb))
        {
            var dir = Vector3.Normalize(other.transform.position - transform.position);
            rb.AddForce(dir * forceWave);
        }

        if (other.gameObject.TryGetComponent<Stats>(out var stats))
            stats.LoseLife(damage);
    }
}