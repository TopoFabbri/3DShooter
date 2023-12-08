using ObjectManagers;
using Patterns.SM;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Controls fireball actions
    /// </summary>
    public class Fireball : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float damage = 25f;
        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private Id stateId;

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
            var trans = transform;
            trans.position += trans.forward * speed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Stats.Stats>(out var stats))
                stats.LoseLife(damage);
        
            FireballManager.Instance.Recycle(gameObject);
        }
    }
}