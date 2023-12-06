using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private int duration = 3;
    
    private void OnEnable()
    {
        StartCoroutine(DestroyOnTime(duration));
    }

    private IEnumerator DestroyOnTime(int time)
    {
        yield return new WaitForSeconds(time);
        ExplosionManager.Instance.Recycle(gameObject);
    }
}