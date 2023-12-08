namespace ObjectManagers
{
    /// <summary>
    /// Controller for all the explosions in the game
    /// </summary>
    public class ExplosionManager : ObjectManager
    {
        private static ExplosionManager instance;

        private ExplosionManager(string id) : base(id)
        {
        }

        public static ExplosionManager Instance
        {
            get
            {
                instance ??= new ExplosionManager("Explosion");

                return instance;
            }
        }
    }
}