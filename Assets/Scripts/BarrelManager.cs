using UnityEngine;

/// <summary>
/// Controller for all the barrels in the game
/// </summary>
public static class BarrelManager
{
    private static readonly ObjectPool Pool = new();

    public static GameObject SpawnBarrel(GameObject barrel, Vector3 pos, Quaternion rot)
    {
        GameObject barrelInstance = Pool.Get();

        if (!barrelInstance) return Factory.Spawn(barrel, pos, rot);
        
        barrelInstance.transform.position = pos;
        barrelInstance.transform.rotation = rot;
            
        return barrelInstance;
    }
    
    public static void RecycleBarrel(GameObject barrel)
    {
        Pool.Release(barrel);
    }
}
