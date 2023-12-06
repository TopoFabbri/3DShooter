namespace ObjectManagers
{
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
