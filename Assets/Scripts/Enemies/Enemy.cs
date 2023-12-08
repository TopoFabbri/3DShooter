using Patterns.SM;
using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Base class for enemies
    /// </summary>
    public abstract class Enemy : MonoBehaviour
    {
        [Header("Enemy:")]
        [SerializeField] protected float speed = 10f;
        [SerializeField] protected float maxSpeed = 10f;
        [SerializeField] protected Rigidbody rb;
        [SerializeField] protected ObstacleEvasion obstacleEvasion;
    
        [SerializeField] private Id stateId;
        [SerializeField] private Stats.Stats stats;

        protected Transform target;
        private const string CharacterName = "Character";
        private StateMachine stateMachine;
    
        protected virtual void Start()
        {
            target = GameObject.Find(CharacterName).transform;
        }

        protected virtual void OnEnable()
        {
            stateMachine = FindObjectOfType<StateMachine>();
            stateMachine.Subscribe(stateId, OnUpdate);
        
            stats.OnDie += DieHandler;
        }

        protected virtual void OnDisable()
        {
            stateMachine.UnSubscribe(stateId, OnUpdate);
        
            stats.OnDie -= DieHandler;
        }
    
        protected virtual void OnUpdate()
        {
            transform.LookAt(target);
        }


        /// <summary>
        /// Manages what to do when enemy dies
        /// </summary>
        protected abstract void DieHandler();
    }
}
