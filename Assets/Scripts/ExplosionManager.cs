/// <summary>
/// Controller for all the explosions in the game
/// </summary>
public class ExplosionManager : ObjectManager
{
    private static ExplosionManager instance;
    
    public static ExplosionManager Instance
    {
        get
        {
            instance ??= new ExplosionManager();

            return instance;
        }
    }
}
