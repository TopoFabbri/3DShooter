using System;
using Level;
using Patterns.SM;
using UnityEngine;

namespace Stats
{
    /// <summary>
    /// Object life stats controller
    /// </summary>
    public class Stats : MonoBehaviour
    {
        [SerializeField] private bool isCharacter;
        [SerializeField] private Transform life;
        [SerializeField] private HUD.Hud hud;
        [SerializeField] private Id stateId;
        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private float initialHp = 100f;

        private float hp;
        private const float LifeRegen = .5f;
        private bool godMode;

        public float InitHp => initialHp;
        
        public event Action OnDie;

        private void OnEnable()
        {
            hp = initialHp;

            if (!stateMachine)
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
            if (hp <= 0)
                Die();

            if (isCharacter)
            {
                hp += LifeRegen * Time.deltaTime;
                
                hp = Mathf.Clamp(hp, 0f, initialHp);
                
                if (hud)
                    hud.SetHealthSlider(hp * 100f / initialHp);
            }

            hp = Mathf.Clamp(hp, 0f, initialHp);

            if (!life) return;

            var lifeTrans = life.transform;
            var scale = lifeTrans.localScale;

            scale.x = hp / (initialHp * 10f);
            lifeTrans.localScale = scale;
        }

        /// <summary>
        /// Damage this object
        /// </summary>
        /// <param name="damage"></param>
        public void LoseLife(float damage)
        {
            if (!godMode)
                hp -= damage;

            if (isCharacter && hud)
                hud.SetHealthSlider(hp);
        }

        /// <summary>
        /// Destroy object
        /// </summary>
        private void Die()
        {
            if (isCharacter)
                LevelManager.Instance.Lose();

            OnDie?.Invoke();
        }

        public void ToggleGodMode()
        {
            godMode = !godMode;
        }
    }
}