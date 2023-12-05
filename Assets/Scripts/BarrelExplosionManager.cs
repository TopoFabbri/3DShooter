/// <summary>
/// Controller for all the barrel explosions in the game
/// </summary>
public class BarrelExplosionManager : ObjectManager
{
    private static BarrelExplosionManager instance;
    
    public static BarrelExplosionManager Instance
    {
        get
        {
            instance ??= new BarrelExplosionManager();

            return instance;
        }
    }
}