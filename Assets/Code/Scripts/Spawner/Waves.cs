using System;
using System.Collections.Generic;
using UnityEngine;
using BoxDefence.AI;

using Random = UnityEngine.Random;

namespace BoxDefence
{
    [Serializable]
    public class Waves
    {
        #region Fields

        [SerializeField] private List<EnemyCount> _enemyPrefabs;

        private List<Enemy> _createdEnemys;

        private List<Transform> _wayPoints;

        private readonly Vector3 OFFSET = new Vector3(0.5f, 0.5f, 0f);

        public event Action<List<Transform>> OnChangeWayPoints;
        public event Action AllEnemyKills;

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

        public void Init(List<Transform> wayPoints)
        {
            ChangeWayPoints(wayPoints);
            _createdEnemys = new List<Enemy>();
        }
        public void ChangeWayPoints(List<Transform> wayPoints)
        {
            _wayPoints = wayPoints;

            OnChangeWayPoints?.Invoke(_wayPoints);
        }
        public void CreateEnemy()
        {
            try
            {
                if (_wayPoints == null)
                    throw new Exception("Waypoints dont a set!");

                foreach(EnemyCount enemy in _enemyPrefabs)
                {
                    for (int i = 0; i < enemy.CoutnEnemy; i++)
                    {
                        Vector3 offset = GetAndCreateRandomOffset();

                        Enemy newEnemy = CreateEnemy(enemy.EnemyPrefab,
                                                     _wayPoints[0].position + offset,
                                                     Quaternion.identity);

                        newEnemy.Init(_wayPoints);
                        newEnemy.OnDead += OnDeadEnemy;
                        OnChangeWayPoints += newEnemy.ChangeWayPoints;

                        _createdEnemys.Add(newEnemy);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
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
        #endregion

        #region Private Methods

        private void OnDeadEnemy(Enemy enemy)
        {
            enemy.OnDead -= OnDeadEnemy;
            OnChangeWayPoints -= enemy.ChangeWayPoints;

            _createdEnemys.Remove(enemy);

            if (_createdEnemys.Count == 0)
                AllEnemyKills?.Invoke();
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
