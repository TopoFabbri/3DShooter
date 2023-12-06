using System;
using UnityEngine;

public class DevilEnemy : Enemy
{
    [Header("Devil:")]
    [SerializeField] private float fireDis = 5f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float longCooldown = 5f;
    [SerializeField] private GameObject fireBall;

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
        
        if (Vector3.Distance(transform.position, target.position) > fireDis)
        {
            obstacleEvasion.CheckAndEvade();
            rb.AddForce(speed * transform.forward, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
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

        nextFireTime = Time.time + (fireCount >= 3 ? longCooldown : cooldown);

        if (fireCount >= 3)
            fireCount = 0;

        var trans = transform;
        
        FireballManager.Instance.Spawn(fireBall, trans.position + trans.forward, trans.rotation);
    }
    
    protected override void DieHandler()
    {
        base.DieHandler();
        
        DevilDestroyed?.Invoke(gameObject);
        EnemyManager.Instance.Recycle(gameObject);
    }
}