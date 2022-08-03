using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace BoxDefence
{
    public class Spawner : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<Transform> _wayPoints;
        [Space]
        [SerializeField] private List<Waves> _waves;
        [Header("Ñharacteristics")]
        [SerializeField] private float _timeBetweenWaves;
        
        private EnemyCounter _enemyCounter;

        #endregion

        #region Actions

        public static event Action OnCreateWaves;

        #endregion

        #region Unity Methods

        private void Start()
        {
            int allEnemy = 0;
            foreach(Waves wave in _waves)
                allEnemy += wave.GetAllegedCountEnemys();

            _enemyCounter = new EnemyCounter(_waves.Count, allEnemy);

            CreateWaves();
        }

        #endregion

        #region Private Methods

        private async void CreateWaves()
        {
            foreach(Waves wave in _waves)
            {
                wave.Init(_wayPoints);
                wave.CreateEnemy();
                wave.AllEnemyKills += _enemyCounter.CountWaves;

                OnCreateWaves?.Invoke();

                await Timer();
            }
        }
        private async Task Timer()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(_timeBetweenWaves);

            await Task.Delay((int)timeSpan.TotalMilliseconds);
        }

        #endregion
    }
}
