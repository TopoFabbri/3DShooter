using System;
using UnityEngine;

public class DevilEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float fireDis = 5f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float longCooldown = 5f;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private Id stateId;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ObstacleEvasion obstacleEvasion;

    private Transform target;
    private float nextFireTime;
    private int fireCount;
    private const string CharacterName = "Character";
    private StateMachine stateMachine;

    public static event Action<GameObject> DevilDestroyed;

    private void OnEnable()
    {
        target = GameObject.Find(CharacterName).transform;
        stateMachine = FindObjectOfType<StateMachine>();
        stateMachine.Subscribe(stateId, OnUpdate);
    }

    private void OnDisable()
    {
        stateMachine.UnSubscribe(stateId, OnUpdate);
    }
    
    /// <summary>
    /// Gameplay-only update
    /// </summary>
    private void OnUpdate()
    {
        transform.LookAt(target);

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
        Instantiate(fireBall, trans.position + trans.forward, trans.rotation);
    }
    
    private void OnDestroy()
    {
        DevilDestroyed?.Invoke(gameObject);
    }
}