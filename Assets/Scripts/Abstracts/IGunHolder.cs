using UnityEngine;
using Weapons;

namespace Abstracts
{
    public interface IGunHolder
    {
        public Gun Weapon { get; set; }
        public Transform GetHand { get; }
        
        /// <summary>
        /// Shoot gun
        /// </summary>
        public void Shoot()
        {
            if (Weapon)
                Weapon.Shoot();
        }
        
        /// <summary>
        /// Give a gun to gun holder
        /// </summary>
        /// <param name="gun">Gun to add</param>
        public void AddGun(Gun gun)
        {
            DropGun();
            gun.GrabGun(GetHand);
            
            Weapon = gun;
        }

        /// <summary>
        /// Drop gun holder's gun
        /// </summary>
        public void DropGun()
        {
            if (Weapon)
                Weapon.DropGun();

            Weapon = null;
        }

        /// <summary>
        /// Reload gun
        /// </summary>
        public void Reload()
        {
            if (Weapon)
                Weapon.Reload();
        }
    }
}
