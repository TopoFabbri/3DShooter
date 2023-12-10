using System;
using System.Collections.Generic;
using Patterns;
using UnityEngine;

namespace ObjectManagers
{
    /// <summary>
    /// Base class for all object managers
    /// </summary>
    [Serializable]
    public abstract class ObjectManager
    {
        protected string id;
        protected struct ScenePool
        {
            public readonly ObjectPool pool;
            public readonly GameObject parent;

            public ScenePool(GameObject prefab)
            {
                pool = new ObjectPool();
                parent = new GameObject(prefab.name + "Pool");
            }
        }
    
        protected readonly Dictionary<string, ScenePool> pools = new();
        private GameObject parent;

        protected ObjectManager(string id = "<Group>Pool")
        {
            this.id = id;
        }

        public virtual GameObject Spawn(GameObject obj, Vector3 pos, Quaternion rot)
        {
            CheckPool(obj);
        
            GameObject objInstance = pools[obj.name].pool.Get();

            if (!objInstance)
                return Factory.Spawn(obj, pos, rot, pools[obj.name].parent.transform);
        
            objInstance.transform.position = pos;
            objInstance.transform.rotation = rot;
        
            objInstance.SetActive(true);
            
            return objInstance;
        }
    
        public virtual void Recycle(GameObject obj)
        {
            string poolName = obj.name;
            if (poolName.Contains("(Clone)"))
                poolName = poolName.Substring(0, poolName.Length - 7);
        
            CheckPool(obj);
        
            pools[poolName].pool.Release(obj);
        }

        protected virtual void InitParent(GameObject prefab)
        {
            if (!parent)
                parent = new GameObject(id + "Pool");
        }

        private void CheckPool(GameObject obj)
        {
            InitParent(obj);

            if (pools.ContainsKey(obj.name))
            {
                if (!pools[obj.name].parent)
                    pools.Remove(obj.name);
            }

            if (pools.ContainsKey(obj.name)) return;
        
            pools.Add(obj.name, new ScenePool(obj));
            pools[obj.name].parent.transform.parent = parent.transform;
        }
    }
}