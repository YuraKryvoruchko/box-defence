using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BoxDefence.PathFinderAI;
using BoxDefence.TimerSystem;
using BoxDefence.Enumerating;

namespace BoxDefence
{
    [Serializable]
    public class Spawner : IEnemyControlPointing
    {
        #region Fields

        [SerializeField] private List<Vector2> _path;
        [SerializeField] private Tilemap _tilemap;
        [Space]
        [SerializeField] private List<Wave> _waves;
        [Header("�haracteristics")]
        [SerializeField] private float _timeBetweenWaves;

        private Vector3 _spawnPoint;

        private TravelPathAgent _travelPathAgent;
        private EnemyWavesCounter _enemyCounter;
        private Timer _timer;

        #endregion

        #region Counstructor

        public void Init(Vector3 spawnPoint)
        {
            _spawnPoint = spawnPoint;
            _travelPathAgent = new TravelPathAgent(_tilemap, _spawnPoint);
            _enemyCounter = new EnemyWavesCounter(_waves.Count);
            _timer = new Timer(_timeBetweenWaves);

            foreach (Wave wave in _waves)
                wave.Init();
        }

        #endregion

        #region Public Methods

        public void SetTilemap(Tilemap tilemap)
        {
            _tilemap = tilemap;
        }
        public void SetSpawnPoint(Vector3 spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }

        public async void CreateWaves()
        {
            CreatePath();

            foreach (Wave wave in _waves)
            {
                wave.OnCreateWave += _enemyCounter.AddWaves;
                wave.ChangePath(_path);
                wave.CreateEnemy(_spawnPoint);

                Debug.Log(_spawnPoint + " wave");

                await _timer.StartTimer();
            }
        }

        public IPassedEnemyCounting GetPassedEnemyCounting()
        {
            PassedEnemyCompositeCounter passedEnemyCompositeCounter = new PassedEnemyCompositeCounter();

            foreach (Wave wave in _waves)
                passedEnemyCompositeCounter.AddPassedEnemyCounting(wave.GetPassedEnemyCounting());

            return passedEnemyCompositeCounter;
        }
        public IDeadEnemyCounting GetDeadEnemyCounting()
        {
            DeadEnemyCompositeCounter deadEnemyCompositeCounter = new DeadEnemyCompositeCounter();

            foreach (Wave wave in _waves)
                deadEnemyCompositeCounter.AddDeadEnemyCounting(wave.GetDeadEnemyCounting());

            return deadEnemyCompositeCounter;
        }
        public IEnemyWavesCounting GetEnemyWavesCounting()
        {
            return _enemyCounter;
        }

        #endregion

        #region Private Methods

        private void CreatePath()
        {
            _path = _travelPathAgent.GetNewPath();
        }

        #endregion
    }
}
