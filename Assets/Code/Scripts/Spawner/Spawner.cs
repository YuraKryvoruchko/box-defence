using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BoxDefence.PathFinderAI;
using BoxDefence.TimerSystem;

using Random = UnityEngine.Random;

namespace BoxDefence
{
    [Serializable]
    public class Spawner : IEnemyControlPointing
    {
        #region Fields

        [SerializeField] private List<Vector2> _path;
        [SerializeField] private Transform _target;
        [SerializeField] private Tilemap _tilemap;
        [Space]
        [SerializeField] private List<Waves> _waves;
        [Header("Ñharacteristics")]
        [SerializeField] private float _timeBetweenWaves;

        private PathFinder _pathFinder;
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

        #endregion

        #region Unity Methods

        public void Init()
        {
            _enemyCounter = new WavesCounter(_waves.Count);
            _timer = new Timer(_timeBetweenWaves);
        }

        #region OnDrawGizmos
        #if UNITY_EDITOR

        public void OnDrawGizmos()
        {
            if(Application.isPlaying == true)
                _pathFinder.OnDrawPath();
        }

        #endif
        #endregion

        #endregion

        #region Public Methods

        public void SetTilemap(Tilemap tilemap)
        {
            _tilemap = tilemap;
        }
        public void SetTarget(Transform target)
        {
            _target = target;
        }

        #endregion

        #region Public Methods

        public async void CreateWaves(Vector3 spawnPosition)
        {
            CreatePath(spawnPosition);

            foreach (Waves wave in _waves)
            {
                wave.OnCreateWave += _enemyCounter.AddWavesToCount;
                wave.Init(_path);
                wave.CreateEnemy(spawnPosition);

                Debug.Log(spawnPosition + " wave");
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

        private void CreatePath(Vector3 spawnPosition)
        {
            Transform target = GetTargetPoint();
            SetTarget(target);

            _pathFinder = new PathFinder(_tilemap);
            _path = _pathFinder.GetPath(spawnPosition, _target.position);
        }
        private Transform GetTargetPoint()
        {
            WayTarget[] wayTargets = MonoBehaviour.FindObjectsOfType<WayTarget>();

            List<Transform> notFreeTargets = new List<Transform>();
            List<Transform> freeTargets = new List<Transform>();
            foreach (WayTarget wayTarget in wayTargets)
            {
                if (wayTarget.IsFree == true)
                    freeTargets.Add(wayTarget.GetTransform());
                else
                    notFreeTargets.Add(wayTarget.GetTransform());
            }

            if(freeTargets.Count > 0)
                return GetRandomTargetTransform(freeTargets);

            return GetRandomTargetTransform(notFreeTargets);
        }
        private Transform GetRandomTargetTransform(List<Transform> targets)
        {
            try
            {
                if (targets.Count == 0)
                    throw new Exception("List count is zero!");

                int index = Random.Range(0, targets.Count - 1);

                Transform target = targets[index];

                return target;
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
