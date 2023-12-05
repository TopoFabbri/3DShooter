/// <summary>
/// Controller for all bullet objects
/// </summary>
public class BulletManager : ObjectManager
{
    private static BulletManager instance;
    
    public static BulletManager Instance
    {
        get
        {
            instance ??= new BulletManager();

            return instance;
        }
    }
}
