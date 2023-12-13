using System;
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
        
        public static event Action<int> OnEnemyDie; 

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
        private void DieHandler()
        {
            AkSoundEngine.PostEvent(Settings.stopSoundEvent, gameObject);

            OnEnemyDie?.Invoke(Settings.score);
            EnemyManager.Instance.Recycle(gameObject);
        }

        public override SpawnableObject SpawnWithSettings(SpawnableSettings settings, Vector3 pos, Quaternion rot)
        {
            Enemy instanced = EnemyManager.Instance.Spawn(gameObject, pos, rot).GetComponent<Enemy>();

            instanced.settings = settings;
            AkSoundEngine.PostEvent(instanced.Settings.playSoundEvent, instanced.gameObject);
            
            return instanced;
        }

        /// <summary>
        /// Handle nuke event
        /// </summary>
        private void OnNukeHandler()
        {
            stats.LoseLife(stats.InitHp);
        }

        /// <summary>
        /// Move enemy in direction
        /// </summary>
        /// <param name="dir">direction to move</param>
        protected virtual void Move(Vector3 dir)
        {
            rb.AddForce(((EnemySettings)settings).speed * dir, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, ((EnemySettings)settings).maxSpeed);
        }

        private void OnDestroy()
        {
            AkSoundEngine.PostEvent(Settings.stopSoundEvent, gameObject);
        }
    }
}