using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoxDefence.Pooling
{
    public class ObjectPoolerStringVersion : MonoBehaviour
    {
        [SerializeField] private List<Pool> _pools;

        private Dictionary<string, Queue<GameObject>> _poolDictionary;

        [Serializable]
        private class Pool
        {
            [field: SerializeField] public string Tag { get; private set; }

            [field: SerializeField] public GameObject PrefabObject { get; private set; }

            [field: SerializeField] public int Size { get; set; }
        }

        #region Singleton
        public static ObjectPoolerStringVersion Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        #endregion

        private void Start()
        {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            CreateObjects();
        }

        public void DeleteObject(string tag, GameObject pool)
        {
            if (_poolDictionary.TryGetValue(tag, out Queue<GameObject> instances))
                instances.Enqueue(pool);
            else
                Debug.LogWarning("Pool with type object: " + pool + " doesn`t excits.");
        }
        public GameObject GetObject(string tag, Vector3 position, Quaternion rotation)
        {
            if (_poolDictionary.TryGetValue(tag, out Queue<GameObject> instances))
            {
                GameObject objectToSpawn = instances.Dequeue();
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;

                return objectToSpawn;
            }
            else
            {
                Debug.LogWarning("Pool with type prefab: " + tag + " doesn`t excits.");

                return default;
            }
        }

        private void CreateObjects()
        {
            foreach (Pool pool in _pools)
            {
                Queue<GameObject> createObjects = new Queue<GameObject>();

                for (int i = 0; i < pool.Size; i++)
                {
                    GameObject instace = Instantiate(pool.PrefabObject);

                    instace.SetActive(false);

                    createObjects.Enqueue(instace);
                }

                _poolDictionary.Add(pool.Tag, createObjects);
            }
        }
    }
}

