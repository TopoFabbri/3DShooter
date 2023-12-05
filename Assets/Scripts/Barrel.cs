using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private GameObject barrelExplosion;
    [SerializeField] private Stats stats;
    
    private Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnEnable()
    {
        stats.OnDie += Explode;
    }
    
    private void OnDisable()
    {
        stats.OnDie -= Explode;
        
        rb.velocity = Vector3.zero;
    }
    
    /// <summary>
    /// Instantiate explosion
    /// </summary>
    private void Explode()
    {
        var trans = transform;
        
        Instantiate(barrelExplosion, trans.position, trans.rotation);
        BarrelManager.Instance.RecycleObject(gameObject);
    }
}