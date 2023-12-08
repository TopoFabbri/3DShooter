namespace ObjectManagers
{
    /// <summary>
    /// Controller for all the pickups in the game
    /// </summary>
    public class PickupManager : ObjectManager
    {
        private static PickupManager instance;

        private PickupManager(string id) : base(id)
        {
        }

        public static PickupManager Instance
        {
            get { return instance ??= new PickupManager("Pickup"); }
        }
    }
}
