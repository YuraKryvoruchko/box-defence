using System;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

namespace BoxDefence.Pooling
{
    public class ObjectPooler : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<Pool> _pools;

        private Dictionary<PoolType, Queue<IPoolObject>> _poolDictionary;

        #endregion

        #region Pool

        [Serializable]
        private class Pool
        {
            [field: SerializeField] public PoolType Type { get; private set; }

            [field: SerializeField] public InterfaceReference<IPoolObject, MonoBehaviour> Prefab { get; private set; }

            [field: SerializeField] public int Size { get; set; }
        }

        #endregion

        #region Singleton

        public static ObjectPooler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            _poolDictionary = new Dictionary<PoolType, Queue<IPoolObject>>();
            
            CreateObjects();
        }

        #endregion

        #region Public Methods

        public void DeleteObject(IPoolObject instace) 
        {
            try
            {
                if (_poolDictionary.TryGetValue(instace.PoolTypeObject, out Queue<IPoolObject> instances) == true)
                    instances.Enqueue(instace);
                else
                    throw new Exception("Pool with type object: " + instace + " doesn`t excits.");
            }
            catch(Exception exception)
            {
                Debug.LogException(exception);
            }
        }
        public T GetObject<T>(T prefab, Vector3 position, Quaternion rotation) where T : IPoolObject
        {
            try
            {
                if (_poolDictionary.TryGetValue(prefab.PoolTypeObject, out Queue<IPoolObject> instances) == false)
                    throw new Exception("Pool with type prefab: " + prefab.PoolTypeObject.ToString() + " doesn`t excits.");

                if (instances.Count == 0)
                    CreateObjectsInQueue(GetPool(prefab.PoolTypeObject), instances);

                IPoolObject objectToSpawn = instances.Dequeue();
                objectToSpawn.Init(position, rotation);

                return (T)objectToSpawn;
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);

                return default;
            }
        }

        #endregion

        #region Private Methods

        private void CreateObjects()
        {
            foreach (Pool pool in _pools)
            {
                Queue<IPoolObject> createObjects = new Queue<IPoolObject>();

                CreateObjectsInQueue(pool, createObjects);

                _poolDictionary.Add(pool.Type, createObjects);
            }
        }
        private void CreateObjectsInQueue(Pool poolObject, Queue<IPoolObject> createObjects)
        {
            for (int i = 0; i < poolObject.Size; i++)
            {
                GameObject instace = Instantiate(poolObject.Prefab.UnderlyingValue.gameObject);

                instace.SetActive(false);

                if (instace.TryGetComponent(out IPoolObject poolPresent))
                    createObjects.Enqueue(poolPresent);
            }
        }

        private Pool GetPool(PoolType poolType)
        {
            try
            {
                foreach (Pool pool in _pools)
                {
                    if (pool.Type == poolType)
                        return pool;
                }

                throw new Exception("Pool with type prefab: " + poolType.ToString() + " doesn`t excits.");
            }
            catch(Exception exception)
            {
                Debug.LogException(exception);

                return default;
            }
        }

        #endregion
    }
}
