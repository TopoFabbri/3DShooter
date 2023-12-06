using ObjectManagers;

/// <summary>
/// Controller for all the fireballs in the game
/// </summary>
public class FireballManager : ObjectManager
{
    private static FireballManager instance;

    private FireballManager(string id) : base(id)
    {
    }
    
    public static FireballManager Instance
    {
        get
        {
            instance ??= new FireballManager("Fireball");

            return instance;
        }
    }
}