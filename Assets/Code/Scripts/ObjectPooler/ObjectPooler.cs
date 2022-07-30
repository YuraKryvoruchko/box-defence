using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoxDefence.Pooling
{
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] private List<Pool> _pools;

        private Dictionary<PoolType, Queue<IPool>> _poolDictionary;

        [Serializable]
        private class Pool
        {
            [field: SerializeField] public PoolType Type { get; private set; }
            [field: SerializeField] public GameObject PrefabObject { get; private set; }

            [field: SerializeField] public int Size { get; set; }
        }

        #region Singleton
        public static ObjectPooler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        #endregion

        private void Start()
        {
            _poolDictionary = new Dictionary<PoolType, Queue<IPool>>();  
            
            CreateObjects();
        }

        public void DeleteObject(GameObject instace)
        {
            if (instace.TryGetComponent(out IPool pool))
            {
                if (_poolDictionary.TryGetValue(pool.PoolTypeObject, out Queue<IPool> instances))
                    instances.Enqueue(pool);
                else
                    Debug.LogWarning("Pool with type object: " + instace + " doesn`t excits.");
            }
        }
        public T GetObject<T>(T prefab, Vector3 position, Quaternion rotation) where T : IPool
        {
            if (_poolDictionary.TryGetValue(prefab.PoolTypeObject, out Queue<IPool> instances))
            {
                IPool objectToSpawn;

                if (instances.Count == 0)
                {
                    foreach (Pool pool in _pools)
                    {
                        if (pool.Type == prefab.PoolTypeObject)
                            CreateObjs(pool);
                    }
                }

                objectToSpawn = instances.Dequeue();

                objectToSpawn.Init(position, rotation);

                return (T)objectToSpawn;
            }
            else
            {
                Debug.LogWarning("Pool with type prefab: " + prefab + " doesn`t excits.");

                return default;
            }
        }

        private void CreateObjects()
        {
            foreach (Pool pool in _pools)
            {
                if (pool.PrefabObject.TryGetComponent(out IPool poolPresent))
                {
                    Queue<IPool> createObjects = new Queue<IPool>();

                    for (int i = 0; i < pool.Size; i++)
                    {
                        GameObject instace = Instantiate(pool.PrefabObject);

                        instace.SetActive(false);

                        if(instace.TryGetComponent(out IPool poolPresent1))
                            createObjects.Enqueue(poolPresent1);
                    }

                    _poolDictionary.Add(poolPresent.PoolTypeObject, createObjects);
                }
            }
        }
        private void CreateObjs(Pool poolObject)
        {
            for (int i = 0; i < poolObject.Size; i++)
            {
                GameObject instace = Instantiate(poolObject.PrefabObject);

                instace.SetActive(false);

                if (instace.TryGetComponent(out IPool poolPresent1))
                {
                    if (_poolDictionary.TryGetValue(poolPresent1.PoolTypeObject, out Queue<IPool> createObjects))
                        createObjects.Enqueue(poolPresent1);
                }
            }
        }
    }
}
