using System;
using System.Collections.Generic;
using UnityEngine;
using BoxDefence.AI;

using Random = UnityEngine.Random;

namespace BoxDefence
{
    [Serializable]
    public class Wave : IDeadEnemyCounterGetting, IPassedEnemyCounterGetting
    {
        #region Fields

        [SerializeField] private List<EnemyCount> _enemyPrefabs;

        private List<Enemy> _createdEnemys;

        private List<Vector2> _path;

        private readonly Vector3 OFFSET = new Vector3(0.5f, 0.5f, 0f);

        private DeadEnemyCounter _deadEnemyCounter;
        private PassedEnemyCounter _passedEnemyCounter;

        #endregion

        #region Actions

        public event Action<List<Vector2>> OnChangePath;
        public event Action OnCreateWave;

        #endregion

        #region EnemyCount

        [Serializable]
        private struct EnemyCount
        {
            [field: SerializeField] public Enemy EnemyPrefab { get; private set; }
            [field: Space]
            [field: SerializeField] public int CoutnEnemy { get; private set; }
        }

        #endregion

        #region Public Methonds

        public void Init()
        {
            _createdEnemys = new List<Enemy>();

            int maxEnemyCount = GetAllegedCountEnemys();
            _deadEnemyCounter = new DeadEnemyCounter(maxEnemyCount);
            _passedEnemyCounter = new PassedEnemyCounter(maxEnemyCount);
        }
        public void CreateEnemy(Vector3 spawnPosition)
        {
            try
            {
                if (_path == null)
                    throw new Exception("Path dont a set!");

                foreach(EnemyCount enemy in _enemyPrefabs)
                {
                    for (int i = 0; i < enemy.CoutnEnemy; i++)
                    {
                        Vector3 offset = GetAndCreateRandomOffset();

                        Enemy newEnemy = CreateEnemy(enemy.EnemyPrefab, spawnPosition + offset, 
                            Quaternion.identity);

                        newEnemy.Init(_path);
                        newEnemy.OnDead += OnDeadEnemy;
                        newEnemy.OnPassed += OnPassedEnemy;
                        OnChangePath += newEnemy.ChangePath;

                        _createdEnemys.Add(newEnemy);
                    }
                }

                OnCreateWave?.Invoke();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }
        public void ChangePath(List<Vector2> path)
        {
            _path = path;

            OnChangePath?.Invoke(_path);
        }
        public int GetAllegedCountEnemys()
        {
            int count = 0;

            foreach(EnemyCount enemyCount in _enemyPrefabs)
                count += enemyCount.CoutnEnemy;

            return count;
        }
        public int GetCountEnemys()
        {
            return _createdEnemys.Count;
        }

        public IDeadEnemyCounting GetDeadEnemyCounting()
        {
            return _deadEnemyCounter;
        }
        public IPassedEnemyCounting GetPassedEnemyCounting()
        {
            return _passedEnemyCounter;
        }

        #endregion

        #region Private Methods

        private void OnDeadEnemy(Enemy enemy)
        {
            UnsubscribeEnemy(enemy);

            _createdEnemys.Remove(enemy);
            _deadEnemyCounter.AddObject();
        }
        private void OnPassedEnemy(Enemy enemy)
        {
            UnsubscribeEnemy(enemy);

            _createdEnemys.Remove(enemy);
            _passedEnemyCounter.AddObject();
        }
        private void UnsubscribeEnemy(Enemy enemy)
        {
            enemy.OnDead -= OnDeadEnemy;
            enemy.OnPassed -= OnPassedEnemy;
            OnChangePath -= enemy.ChangePath;
        }
        private Enemy CreateEnemy(Enemy prefab, Vector3 position, Quaternion quaternion)
        {
            Enemy newEnemy = MonoBehaviour.Instantiate(prefab,
                                                       position,
                                                       quaternion);

            return newEnemy;
        }
        private Vector3 GetAndCreateRandomOffset()
        {
            float xPosition = Random.Range(-OFFSET.x, OFFSET.x);
            float yPosition = Random.Range(-OFFSET.y, OFFSET.y);

            Vector3 offset = new Vector3(xPosition, yPosition);

            return offset;
        }

        #endregion
    }
}
