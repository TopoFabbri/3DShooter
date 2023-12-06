/// <summary>
/// Controller for all the lethal explosions in the game
/// </summary>
public class LethalExplosionManager : ObjectManager
{
    private static LethalExplosionManager instance;

    private LethalExplosionManager(string id) : base(id)
    {
    }

    public static LethalExplosionManager Instance
    {
        get
        {
            instance ??= new LethalExplosionManager("LethalExplosion");

            return instance;
        }
    }
}