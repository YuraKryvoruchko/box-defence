using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BoxDefence.PathFinderAI;

using Random = UnityEngine.Random;

namespace BoxDefence
{
    public class Spawner : MonoBehaviour
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

            Transform target = GetTargetPoint();
            SetTarget(target);

            _enemyCounter = new EnemyCounter(_waves.Count, allEnemy);
            _pathFinder = new PathFinder(_tilemap);
            _path = _pathFinder.GetPath(transform.position, _target.position);

            CreateWaves();
        }

        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if(Application.isPlaying == true)
                _pathFinder.OnDrawPath();
        }

        #endif

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

        #region Private Methods

        private async void CreateWaves()
        {
            foreach(Waves wave in _waves)
            {
                wave.Init(_path);
                wave.CreateEnemy(transform.position);
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
        private Transform GetTargetPoint()
        {
            WayTarget[] wayTargets = FindObjectsOfType<WayTarget>();

            List<Transform> notFreeTragets = new List<Transform>();
            List<Transform> freeTargets = new List<Transform>();
            foreach (WayTarget wayTarget in wayTargets)
            {
                if (wayTarget.IsFree == true)
                    freeTargets.Add(wayTarget.GetTransform());
                else
                    notFreeTragets.Add(wayTarget.GetTransform());
            }

            if (freeTargets.Count == 0)
            {
                foreach (WayTarget wayTarget in wayTargets)
                    notFreeTragets.Add(wayTarget.GetTransform());

                return GetRandomTargetTransform(notFreeTragets);
            }

            return GetRandomTargetTransform(freeTargets);
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
