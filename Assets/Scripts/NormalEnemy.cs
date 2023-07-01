using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damageTime = 2f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private Id stateId;
    [SerializeField] private string characterTag;

    [SerializeField] private ObstacleEvasion obstacleEvasion;
    [SerializeField] private Rigidbody rb;
    private Transform target;
    private float cooldown;
    private StateMachine stateMachine;
    private const string CharacterName = "Character";
    
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
        rb.velocity = speed * transform.forward;
    }

    private void OnCollisionStay(Collision other)
    {
        if (!(Time.time > cooldown) || !other.gameObject.CompareTag(characterTag)) return;
        
        other.gameObject.GetComponent<Stats>().LoseLife(damage);
        cooldown = Time.time + damageTime;
    }
}