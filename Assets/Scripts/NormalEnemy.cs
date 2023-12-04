using System;
using UnityEngine;

public class NormalEnemy : Enemy
{
    [Header("Normal:")]
    [SerializeField] private float damageTime = 2f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private string characterTag;
    
    private float cooldown;
    private bool isInCooldown;
    
    public static event Action<GameObject> ZombieDestroyed; 
    
    /// <summary>
    /// Gameplay-only update
    /// </summary>
    protected override void OnUpdate()
    {
        base.OnUpdate();

        obstacleEvasion.CheckAndEvade();
        rb.AddForce(speed * transform.forward, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void OnCollisionStay(Collision other)
    {
        if (!(Time.time > cooldown && other.gameObject.CompareTag(characterTag))) return;
        
        if (other.gameObject.TryGetComponent<Stats>(out var otherStats))
            otherStats.LoseLife(damage);
        cooldown = Time.time + damageTime;
    }
    
    protected override void DieHandler()
    {
        base.DieHandler();
        
        ZombieDestroyed?.Invoke(gameObject);
        Destroy(gameObject);
    }
}