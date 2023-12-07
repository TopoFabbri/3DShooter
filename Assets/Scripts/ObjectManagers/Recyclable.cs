using System.Collections.Generic;
using UnityEngine;

namespace ObjectManagers
{
    [CreateAssetMenu(fileName = "Recyclable", menuName = "SOs/Recyclables")]
    public class Recyclable : ScriptableObject
    {
        public enum Manager
        {
            Bullet,
            Enemy,
            Explosion,
            Fireball,
            LethalExplosion,
            Lethal,
            Pickup
        }

        public GameObject prefab;
        public Manager selectManager;

        private static Dictionary<Manager, ObjectManager> managers = new();

        private string id;

        public GameObject Get(Vector3 pos, Quaternion rot) => managers[selectManager].Spawn(prefab, pos, rot);
        
        public static void Init()
        {
            if (managers.Count > 0) return;
            
            managers.Add(Manager.Bullet, BulletManager.Instance);
            managers.Add(Manager.Enemy, EnemyManager.Instance);
            managers.Add(Manager.Explosion, ExplosionManager.Instance);
            managers.Add(Manager.Fireball, FireballManager.Instance);
            managers.Add(Manager.LethalExplosion, LethalExplosionManager.Instance);
            managers.Add(Manager.Lethal, LethalManager.Instance);
            managers.Add(Manager.Pickup, PickupManager.Instance);
        }
    }
}
