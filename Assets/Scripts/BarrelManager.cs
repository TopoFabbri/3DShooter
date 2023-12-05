/// <summary>
/// Controller for all the barrels in the game
/// </summary>
public class BarrelManager : ObjectManager
{
    private static BarrelManager instance;
    
    public static BarrelManager Instance
    {
        get
        {
            instance ??= new BarrelManager();

            return instance;
        }
    }
}
