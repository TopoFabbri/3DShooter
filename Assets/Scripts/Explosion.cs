using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private int duration = 3;
    
    private void Start()
    {
        StartCoroutine(DestroyOnTime(duration));
    }

    private IEnumerator DestroyOnTime(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}