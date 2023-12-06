namespace ObjectManagers
{
    /// <summary>
    /// Controller for all the lethals in the game
    /// </summary>
    public class LethalManager : ObjectManager
    {
        private static LethalManager instance;

        public static LethalManager Instance
        {
            get { return instance ??= new LethalManager("Lethal"); }
        }

        private LethalManager(string id = "<Group>Pool") : base(id)
        {
        }
    }
}