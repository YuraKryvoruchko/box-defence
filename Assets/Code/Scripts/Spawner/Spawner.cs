using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BoxDefence.PathFinderAI;
using BoxDefence.TimerSystem;

namespace BoxDefence
{
    [Serializable]
    public class Spawner : IEnemyControlPointing
    {
        #region Fields

        [SerializeField] private List<Vector2> _path;
        [SerializeField] private Tilemap _tilemap;
        [Space]
        [SerializeField] private List<Waves> _waves;
        [Header("Ñharacteristics")]
        [SerializeField] private float _timeBetweenWaves;

        private Vector3 _spawnPoint;

        private TravelPathAgent _travelPathAgent;
        private WavesCounter _enemyCounter;
        private Timer _timer;

        #endregion

        #region Actions

        public event Action OnAddPassedEnemy;
        public event Action OnRemovePassedEnemy;
        public event Action OnAddDeadEnemy;
        public event Action OnRemoveDeadEnemy;
        public event Action OnAddEnemyWaves;
        public event Action OnRemoveEnemyWaves;

        #endregion

        #region Counstructor

        public Spawner(Tilemap tilemap, List<Waves> waves)
        {
            SetTilemap(tilemap);

            _waves = waves;

            _enemyCounter = new WavesCounter(_waves.Count);
            _timer = new Timer(_timeBetweenWaves);
        }
        public void Init(Vector3 spawnPoint)
        {
            _spawnPoint = spawnPoint;
            _travelPathAgent = new TravelPathAgent(_tilemap, _spawnPoint);
            _enemyCounter = new WavesCounter(_waves.Count);
            _timer = new Timer(_timeBetweenWaves);
        }

        #endregion

        #region Unity Methods


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

        #endregion

        #region Public Methods

        public async void CreateWaves()
        {
            CreatePath();

            foreach (Waves wave in _waves)
            {
                wave.OnCreateWave += _enemyCounter.AddWavesToCount;
                wave.Init(_path);
                wave.CreateEnemy(_spawnPoint);

                Debug.Log(_spawnPoint + " wave");
                OnAddEnemyWaves?.Invoke();

                await _timer.StartTimer();
            }
        }

        public int GetDeadEnemyCount()
        {
            throw new NotImplementedException();
        }
        public int GetEnemyWavesCount()
        {
            return _enemyCounter.GetCurrentWavesCount();
        }
        public int GetMaxDeadEnemyCount()
        {
            throw new NotImplementedException();
        }
        public int GetMaxEnemyWavesCount()
        {
            return _enemyCounter.GetMaxWavesCount();
        }
        public int GetMaxPassedEnemyCount()
        {
            throw new NotImplementedException();
        }
        public int GetPassedEnemyCount()
        {
            throw new NotImplementedException();
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
