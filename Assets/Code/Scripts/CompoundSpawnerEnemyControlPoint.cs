using System;
using System.Collections.Generic;
using UnityEngine;
using BoxDefence.Tilemaps;

namespace BoxDefence
{
    public class CompoundSpawnerEnemyControlPoint : MonoBehaviour, IEnemyControlPointing
    {
        #region Fields

        [SerializeField] private List<SpawnTile> _spawnerTiles;

        private List<IEnemyControlPointing> _enemyControlPoints;

        #endregion

        #region Actions

        public event Action OnAddPassedEnemy;
        public event Action OnRemovePassedEnemy;
        public event Action OnAddDeadEnemy;
        public event Action OnRemoveDeadEnemy;
        public event Action OnAddEnemyWaves;
        public event Action OnRemoveEnemyWaves;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _enemyControlPoints = new List<IEnemyControlPointing>();
        }
        private void OnEnable()
        {
            foreach (SpawnTile spawnTile in _spawnerTiles)
                spawnTile.OnCreateSpanwer += AddEnemyControlPoint;
        }
        private void OnDisable()
        {
            foreach (SpawnTile spawnTile in _spawnerTiles)
                spawnTile.OnCreateSpanwer -= AddEnemyControlPoint;
        }

        #endregion

        #region Public Methods

        public void AddEnemyControlPoint(IEnemyControlPointing enemyControlPoint)
        {
            if (_enemyControlPoints.Contains(enemyControlPoint) == true)
                throw new Exception("Spawner is contains!");

            _enemyControlPoints.Add(enemyControlPoint);
            enemyControlPoint.OnAddDeadEnemy += () => OnAddDeadEnemy?.Invoke();
            enemyControlPoint.OnAddEnemyWaves += () => OnAddEnemyWaves?.Invoke();
            enemyControlPoint.OnAddPassedEnemy += () => OnAddPassedEnemy?.Invoke();
            enemyControlPoint.OnRemoveDeadEnemy += () => OnRemoveDeadEnemy?.Invoke();
            enemyControlPoint.OnRemoveEnemyWaves += () => OnRemoveEnemyWaves?.Invoke();
            enemyControlPoint.OnRemovePassedEnemy += () => OnRemovePassedEnemy?.Invoke();
        }
        public void Remove(IEnemyControlPointing enemyControlPoint)
        {
            if (_enemyControlPoints.Contains(enemyControlPoint) == true)
                throw new Exception("Spawner is contains!");

            _enemyControlPoints.Remove(enemyControlPoint);
            enemyControlPoint.OnAddDeadEnemy -= () => OnAddDeadEnemy?.Invoke();
            enemyControlPoint.OnAddEnemyWaves -= () => OnAddEnemyWaves?.Invoke();
            enemyControlPoint.OnAddPassedEnemy -= () => OnAddPassedEnemy?.Invoke();
            enemyControlPoint.OnRemoveDeadEnemy -= () => OnRemoveDeadEnemy?.Invoke();
            enemyControlPoint.OnRemoveEnemyWaves -= () => OnRemoveEnemyWaves?.Invoke();
            enemyControlPoint.OnRemovePassedEnemy -= () => OnRemovePassedEnemy?.Invoke();
        }

        public int GetPassedEnemyCount()
        {
            int passedEnemyCount = 0;

            foreach (IEnemyControlPointing enemyControlPointing in _enemyControlPoints)
                passedEnemyCount += enemyControlPointing.GetPassedEnemyCount();

            return passedEnemyCount;
        }
        public int GetMaxPassedEnemyCount()
        {
            int maxPassedEnemyCount = 0;

            foreach (IEnemyControlPointing enemyControlPointing in _enemyControlPoints)
                maxPassedEnemyCount += enemyControlPointing.GetMaxPassedEnemyCount();

            return maxPassedEnemyCount;
        }
        public int GetDeadEnemyCount()
        {
            int deadEnemyCount = 0;

            foreach (IEnemyControlPointing enemyControlPointing in _enemyControlPoints)
                deadEnemyCount += enemyControlPointing.GetDeadEnemyCount();

            return deadEnemyCount;
        }
        public int GetMaxDeadEnemyCount()
        {
            int maxDeadEnemyCount = 0;

            foreach (IEnemyControlPointing enemyControlPointing in _enemyControlPoints)
                maxDeadEnemyCount += enemyControlPointing.GetMaxDeadEnemyCount();

            return maxDeadEnemyCount;
        }
        public int GetEnemyWavesCount()
        {
            int enemyWavesCount = 0;

            foreach (IEnemyControlPointing enemyControlPointing in _enemyControlPoints)
                enemyWavesCount += enemyControlPointing.GetEnemyWavesCount();

            return enemyWavesCount;
        }
        public int GetMaxEnemyWavesCount()
        {
            int maxEnemyWavesCount = 0;

            foreach (IEnemyControlPointing enemyControlPointing in _enemyControlPoints)
                maxEnemyWavesCount += enemyControlPointing.GetMaxEnemyWavesCount();

            return maxEnemyWavesCount;
        }

        #endregion
    }
}
