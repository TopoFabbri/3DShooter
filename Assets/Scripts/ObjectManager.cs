using UnityEngine;

/// <summary>
/// Base class for all object managers
/// </summary>
public abstract class ObjectManager
{
    private readonly ObjectPool pool = new();
    private GameObject parent;

    public virtual GameObject SpawnObject(GameObject obj, Vector3 pos, Quaternion rot)
    {
        GameObject objInstance = pool.Get();

        if (!objInstance)
        {
            InitParent(obj);
            return Factory.Spawn(obj, pos, rot, parent.transform);
        }
        
        objInstance.transform.position = pos;
        objInstance.transform.rotation = rot;
        
        objInstance.SetActive(true);
            
        return objInstance;
    }
    
    public virtual void RecycleObject(GameObject obj)
    {
        pool.Release(obj);
    }

    protected virtual void InitParent(GameObject prefab)
    {
        if (!parent)
            parent = new GameObject(prefab.name + "Pool");
    }
}