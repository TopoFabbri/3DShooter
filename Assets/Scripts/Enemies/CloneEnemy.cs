using Abstracts;
using SOs;
using UnityEngine;
using Weapons;

namespace Enemies
{
    /// <summary>
    /// Control clone actions
    /// </summary>
    public class CloneEnemy : Enemy, IGunHolder
    {
        CloneSettings Settings => settings as CloneSettings;

        [SerializeField] private Transform hand;
        
        public Gun Weapon { get; set; }

        public Transform GetHand => hand;

        protected override void OnUpdate()
        {
            if (!Weapon)
                LookForWeapon();
            else if (Vector3.Distance(transform.position, Settings.target.position) > Settings.shootDis)
                MoveToPlayer();
            else
                Fire();
            
            if (Weapon)
                Weapon.transform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Look for the closest weapon
        /// </summary>
        private void LookForWeapon()
        {
            if (WeaponPickup.weaponsInMap.Count == 0)
                return;

            Transform closestWeapon = null;
            
            foreach (WeaponPickup weaponPickup in WeaponPickup.weaponsInMap)
            {
                if (!weaponPickup.gameObject.activeSelf)
                    continue;
                
                if (!closestWeapon)
                {
                    closestWeapon = weaponPickup.transform;
                    continue;
                }

                Vector3 position = transform.position;
                
                float closestDistance = Vector3.Distance(position, closestWeapon.position);
                float currentDistance = Vector3.Distance(position, weaponPickup.transform.position);
                
                if (currentDistance < closestDistance)
                    closestWeapon = weaponPickup.transform;
            }
            
            if (!closestWeapon)
                return;

            Transform trans = transform;
            Vector3 closestWeaponPos = closestWeapon.position;
            
            closestWeaponPos.y = trans.position.y;
            closestWeapon.position = closestWeaponPos;

            trans.LookAt(closestWeapon);
            
            obstacleEvasion.CheckAndEvade();
            Move(trans.forward);
        }

        /// <summary>
        /// Move towards player
        /// </summary>
        private void MoveToPlayer()
        {
            Transform trans;
            (trans = transform).LookAt(Settings.target);
            
            obstacleEvasion.CheckAndEvade();
            Move(trans.forward);
        }

        /// <summary>
        /// Aim and shoot towards player
        /// </summary>
        private void Fire()
        {
            if (Weapon.IsReloading || Weapon.InCooldown)
                return;
            
            Vector3 spread = Random.insideUnitSphere * Settings.shootSpread;
            
            Weapon.transform.LookAt(Settings.target.position + spread);
            
            transform.LookAt(Settings.target);
            ((IGunHolder)this).Shoot();
        }
    }
}