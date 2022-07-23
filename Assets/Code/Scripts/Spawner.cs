using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxDefence.AI;

using Random = UnityEngine.Random;

namespace BoxDefence
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _wayPoints;
        [Space]
        [SerializeField] private Enemy[] _enemiesPrefab;
        [Header("Ñharacteristics")]
        [SerializeField] private int _maxWavesCount;
        [SerializeField] private int _countEnemyInOneWaves = 5;
        [Space]
        [SerializeField] private Vector2 _offset;
        [Space]
        [SerializeField] private float _timeBetweenWaves;
        
        private EnemyCounter _enemyCounter;

        [SerializeField] private int _createdWavesCount = 0;

        private bool _canCreateWaves = true;

        public static event Action OnCreateWaves;

        private void Start()
        {
            int allEnemy = _maxWavesCount * _countEnemyInOneWaves;

            _enemyCounter = new EnemyCounter(_maxWavesCount, allEnemy);
        }
        private void Update()
        {
            if (_canCreateWaves == true && _createdWavesCount < _maxWavesCount)
            {
                CreateWaves();

                _canCreateWaves = false;

                StartCoroutine(TimerForShoot());
            }
        }

        private void CreateWaves()
        {
            for(int i = 0; i < _countEnemyInOneWaves; i++)
            {
                foreach (Enemy enemy in _enemiesPrefab)
                {
                    Enemy currentEnemy = Instantiate(enemy, transform, false);

                    currentEnemy.Init(_wayPoints);

                    float xPosition = Random.Range(-_offset.x, _offset.x);
                    float yPosition = Random.Range(-_offset.y, _offset.y);

                    Vector2 offset = new Vector2(xPosition, yPosition);

                    currentEnemy.transform.position += (Vector3)offset;

                    currentEnemy.OnDead += _enemyCounter.CountEnemy;
                }
            }

            OnCreateWaves?.Invoke();

            _createdWavesCount++;
        }

        private IEnumerator TimerForShoot()
        {
            yield return new WaitForSeconds(_timeBetweenWaves);

            _canCreateWaves = true;
        }
    }
}
