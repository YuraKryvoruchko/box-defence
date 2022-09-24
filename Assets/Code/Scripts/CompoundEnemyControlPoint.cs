using System;
using System.Collections.Generic;
using UnityEngine;
using BoxDefence.Tilemaps;

namespace BoxDefence
{
    public class CompoundEnemyControlPoint : MonoBehaviour, IEnemyControlPointing
    {
        #region Fields

        [SerializeField] private List<SpawnTile> _spawnerTiles;

        private List<IEnemyControlPointing> _enemyControlPoints;

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
        }
        public void Remove(IEnemyControlPointing enemyControlPoint)
        {
            if (_enemyControlPoints.Contains(enemyControlPoint) == true)
                throw new Exception("Spawner is contains!");

            _enemyControlPoints.Remove(enemyControlPoint);
        }

        public IPassedEnemyCounting GetPassedEnemyCounting()
        {
            throw new NotImplementedException();
        }
        public IDeadEnemyCounting GetDeadEnemyCounting()
        {
            throw new NotImplementedException();
        }
        public IEnemyWavesCounting GetEnemyWavesCounting()
        {
            int currentWavesCount = 0;
            int maxWavesCount = 0;

            foreach(IEnemyControlPointing enemyControlPointing in _enemyControlPoints)
            {
                IEnemyWavesCounting enemyWavesCounting = enemyControlPointing.GetEnemyWavesCounting();

                currentWavesCount += enemyWavesCounting.GetEnemyWavesCount();
                maxWavesCount += enemyWavesCounting.GetMaxEnemyWavesCount();
            }

            EnemyWavesCounter newWavesCounting = new EnemyWavesCounter(maxWavesCount, currentWavesCount);

            foreach (IEnemyControlPointing enemyControlPointing in _enemyControlPoints)
            {
                IEnemyWavesCounting enemyWavesCounting = enemyControlPointing.GetEnemyWavesCounting();

                enemyWavesCounting.OnAddEnemyWaves += newWavesCounting.AddWaves;
                enemyWavesCounting.OnRemoveEnemyWaves += newWavesCounting.RemoveWaves;
            }

            return newWavesCounting;
        }

        #endregion
    }
}
