/// <summary>
/// Controller for all the enemies in the game
/// </summary>
public class EnemyManager : ObjectManager
{
    private static EnemyManager instance;

    private EnemyManager(string id) : base(id)  
    {
    }

    public static EnemyManager Instance
    {
        get
        {
            instance ??= new EnemyManager("Enemy");

            return instance;
        }
    }
}
