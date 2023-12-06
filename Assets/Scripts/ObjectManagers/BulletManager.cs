using ObjectManagers;

/// <summary>
/// Controller for all bullet objects
/// </summary>
public class BulletManager : ObjectManager
{
    private static BulletManager instance;

    private BulletManager(string id) : base(id)
    {
    }

    public static BulletManager Instance
    {
        get
        {
            instance ??= new BulletManager("Bullet");

            return instance;
        }
    }
}