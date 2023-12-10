using UnityEngine;

namespace ObjectManagers
{
    /// <summary>
    /// Controller for all the enemies in the game
    /// </summary>
    public class EnemyManager : ObjectManager
    {
        private static EnemyManager instance;

        private EnemyManager(string id) : base(id)  
        {
        }

        public static EnemyManager Instance
        {
            get
            {
                instance ??= new EnemyManager("Enemy");

                return instance;
            }
        }

        private int enemiesAlive;

        public static int EnemiesAlive => Instance.enemiesAlive;

        public override GameObject Spawn(GameObject obj, Vector3 pos, Quaternion rot)
        {
            enemiesAlive++;
            return base.Spawn(obj, pos, rot);
        }

        public override void Recycle(GameObject obj)
        {
            enemiesAlive--;
            base.Recycle(obj);
        }
    }
}
