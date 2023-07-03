using System;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float damageTime = 2f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private Id stateId;
    [SerializeField] private string characterTag;
    [SerializeField] private ObstacleEvasion obstacleEvasion;
    [SerializeField] private Rigidbody rb;

    private Transform target;
    private float cooldown;
    private bool isInCooldown;
    private StateMachine stateMachine;
    private const string CharacterName = "Character";
    
    public static event Action<GameObject> ZombieDestroyed; 

    private void Start()
    {
        target = GameObject.Find(CharacterName).transform;
    }

    private void OnEnable()
    {
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

        obstacleEvasion.CheckAndEvade();
        rb.AddForce(speed * transform.forward, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void OnCollisionStay(Collision other)
    {
        if (!(Time.time > cooldown && other.gameObject.CompareTag(characterTag))) return;
        
        if (other.gameObject.TryGetComponent<Stats>(out var stats))
            stats.LoseLife(damage);
        cooldown = Time.time + damageTime;
    }

    private void OnDestroy()
    {
        ZombieDestroyed?.Invoke(gameObject);
    }
}