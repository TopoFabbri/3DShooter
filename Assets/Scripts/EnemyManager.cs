/// <summary>
/// Controller for all the enemies in the game
/// </summary>
public class EnemyManager : ObjectManager
{
    private static EnemyManager instance;
    
    public static EnemyManager Instance
    {
        get
        {
            instance ??= new EnemyManager();

            return instance;
        }
    }
}
