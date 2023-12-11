using Weapons;

namespace Abstracts
{
    public interface IGunHolder
    {
        /// <summary>
        /// Shoot gun
        /// </summary>
        public void Shoot();
        /// <summary>
        /// Give a gun to gun holder
        /// </summary>
        /// <param name="gun">Gun to add</param>
        public void AddGun(Gun gun);
        /// <summary>
        /// Drop gun holder's gun
        /// </summary>
        public void DropGun();
        /// <summary>
        /// Reload gun
        /// </summary>
        public void Reload();
    }
}
