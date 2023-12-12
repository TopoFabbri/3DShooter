using System.Collections;
using FX;
using Patterns.SM;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons
{
    /// <summary>
    /// Base class for all weapons
    /// </summary>
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected GameObject hand;
        [SerializeField] protected Transform bulletSpawnPoint;
        [SerializeField] protected StateMachine stateMachine;
        [SerializeField] protected Id stateId;
        [SerializeField] protected Rigidbody rb;
        [SerializeField] protected BoxCollider boxCollider;
        [SerializeField] protected WeaponVFX weaponVFX;

        [SerializeField] private float bulletReloadTime;
        [SerializeField] private float minBetweenShotTime;
        [SerializeField] private Vector3 dropForce;
    
        private const int ChamberSize = 6;

        private int bullets;

        public bool IsReloading { get; private set; }
        public bool InCooldown { get; private set; }
        
        public int Chamber 
        {
            get => bullets;
            private set => bullets = Mathf.Clamp(value, 0, ChamberSize);
        }
        
        protected virtual void OnEnable()
        {
            if (!stateMachine)
                stateMachine = GameObject.Find("StateMachine").GetComponent<StateMachine>();
                
            stateMachine.Subscribe(stateId, OnUpdate);
        }

        protected virtual void OnDisable()
        {
            stateMachine.UnSubscribe(stateId, OnUpdate);
        }
        
        protected virtual void OnUpdate()
        {
            CheckReload();
        }

        /// <summary>
        /// Gameplay-only update
        /// </summary>
        private void CheckReload()
        {
            if (Chamber <= 0)
                Reload();
        }

        /// <summary>
        /// Start action reload
        /// </summary>
        public void Reload()
        {
            if (IsReloading) return;
    
            IsReloading = true;
            StartCoroutine(StopReloadOnTime(bulletReloadTime * (ChamberSize - Chamber)));
        
            Chamber = ChamberSize;
        }
        
        /// <summary>
        /// Wait for 'time' then stop reload action
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private IEnumerator StopReloadOnTime(float time)
        {
            yield return new WaitForSeconds(time);
            IsReloading = false;
        }

        private IEnumerator StopCooldownOnTime(float time)
        {
            yield return new WaitForSeconds(time);
            InCooldown = false;
        }
    
        /// <summary>
        /// Release grabbed weapon
        /// </summary>
        public void DropGun()
        {
            var trans = transform;
        
            rb.useGravity = true;
            boxCollider.isTrigger = false;
            hand.SetActive(false);
            trans.parent = null;
            rb.AddForce(trans.forward * dropForce.z + trans.up * dropForce.y, ForceMode.Impulse);
        }

        /// <summary>
        /// Try and select a gun from the floor and set as current weapon
        /// </summary>
        /// <param name="parent"></param>
        public void GrabGun(Transform parent)
        {
            rb.useGravity = false;
            boxCollider.isTrigger = true;
            hand.SetActive(true);
            transform.parent = parent;
            transform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Call action shoot
        /// </summary>
        public virtual void Shoot()
        {
            if (IsReloading || InCooldown) return;
            
            if (Chamber <= 0)
            {
                Reload();
                return;
            }
            
            Chamber--;
        
            weaponVFX.Shoot();
            InCooldown = true;
            StartCoroutine(StopCooldownOnTime(minBetweenShotTime));
        }
    }
}