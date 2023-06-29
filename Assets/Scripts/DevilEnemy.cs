using UnityEngine;

[RequireComponent(typeof(ObstacleEvasion))]
public class DevilEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float fireDis = 5f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float longCooldown = 5f;
    [SerializeField] private GameObject fireBall;

    private ObstacleEvasion obstacleEvasion;
    private Transform target;
    private Rigidbody rb;
    private float nextFireTime;
    private int fireCount;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private Id stateId;
    private const string CharacterName = "Character";

    private void Start()
    {
        obstacleEvasion = GetComponent<ObstacleEvasion>();
        target = GameObject.Find(CharacterName).transform;
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
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

        //TODO: Fix - Could be a coroutine
        if (Time.time > nextFireTime)
        {
            if (Vector3.Distance(transform.position, target.position) > fireDis)
            {
                obstacleEvasion.CheckAndEvade();
                rb.velocity = speed * transform.forward;
            }
            else
            {
                Shoot();
            }
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
}