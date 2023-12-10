using Abstracts;
using Game;
using ObjectManagers;
using Patterns.SM;
using SOs;
using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Base class for enemies
    /// </summary>
    public abstract class Enemy : SpawnableObject
    {
        [SerializeField] protected Rigidbody rb;
        [SerializeField] protected ObstacleEvasion obstacleEvasion;
        [SerializeField] private Id stateId;

        [SerializeField] private Stats.Stats stats;

        private StateMachine stateMachine;

        private EnemySettings Settings => settings as EnemySettings;

        protected virtual void Start()
        {
            Settings.target = GameObject.Find(Settings.characterName).transform;
        }

        protected virtual void OnEnable()
        {
            stateMachine = FindObjectOfType<StateMachine>();
            stateMachine.Subscribe(stateId, OnUpdate);
            
            Cheats.Nuke += OnNukeHandler;

            stats.OnDie += DieHandler;
        }

        protected virtual void OnDisable()
        {
            stateMachine.UnSubscribe(stateId, OnUpdate);

            Cheats.Nuke -= OnNukeHandler;
            
            stats.OnDie -= DieHandler;
        }

        protected virtual void OnUpdate()
        {
            transform.LookAt(Settings.target);
        }

        /// <summary>
        /// Manages what to do when enemy dies
        /// </summary>
        protected abstract void DieHandler();

        public override void SpawnWithSettings(SpawnableSettings settings, Vector3 pos, Quaternion rot)
        {
            Enemy instanced = EnemyManager.Instance.Spawn(gameObject, pos, rot).GetComponent<Enemy>();

            instanced.settings = settings;
        }

        private void OnNukeHandler()
        {
            stats.LoseLife(stats.InitHp);
        }
    }
}