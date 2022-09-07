using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BoxDefence.PathFinderAI;
using BoxDefence.TimerSystem;

using Random = UnityEngine.Random;

namespace BoxDefence
{
    public class Spawner : MonoBehaviour, ISetingEnumerator, ISpawner
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

        public static event Action OnCreateWaves;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _enemyCounter = new WavesCounter(_waves.Count);
            _timer = new Timer(_timeBetweenWaves);
        }
        private void Start()
        {
            Transform target = GetTargetPoint();
            SetTarget(target);

            _pathFinder = new PathFinder(_tilemap);
            _path = _pathFinder.GetPath(transform.position, _target.position);

            CreateWaves();
        }

        #region OnDrawGizmos
        #if UNITY_EDITOR

        private void OnDrawGizmos()
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
        public WavesCounter GetWavesCounter()
        {
            return _enemyCounter;
        }

        #endregion

        #region Private Methods

        private async void CreateWaves()
        {
            foreach(Waves wave in _waves)
            {
                wave.OnCreateWave += _enemyCounter.AddWavesToCount;
                wave.Init(_path);
                wave.CreateEnemy(transform.position);

                OnCreateWaves?.Invoke();

                await _timer.StartTimer();
            }
        }
        private Transform GetTargetPoint()
        {
            WayTarget[] wayTargets = FindObjectsOfType<WayTarget>();

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
    public interface ISetingEnumerator
    {
        public WavesCounter GetWavesCounter();
    }
    public interface ISpawner
    {
        WavesCounter GetWavesCounter();
    }
}
