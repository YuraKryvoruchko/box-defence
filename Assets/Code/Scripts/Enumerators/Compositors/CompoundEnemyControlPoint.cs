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
                spawnTile.OnBroadcastEnemyBase += AddEnemyControlPoint;
        }
        private void OnDisable()
        {
            foreach (SpawnTile spawnTile in _spawnerTiles)
                spawnTile.OnBroadcastEnemyBase -= AddEnemyControlPoint;
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
            PassedEnemyCompositeCounter passedEnemyCompositeCounter 
                = new PassedEnemyCompositeCounter();

            foreach (IEnemyControlPointing enemyControlPointing in _enemyControlPoints)
            {
                IPassedEnemyCounting passedEnemyCounting = 
                    enemyControlPointing.GetPassedEnemyCounting();

                passedEnemyCompositeCounter.AddPassedEnemyCounting(passedEnemyCounting);
            }

            return passedEnemyCompositeCounter;
        }
        public IDeadEnemyCounting GetDeadEnemyCounting()
        {
            DeadEnemyCompositeCounter passedEnemyCompositeCounter
                = new DeadEnemyCompositeCounter();

            foreach (IEnemyControlPointing enemyControlPointing in _enemyControlPoints)
            {
                IDeadEnemyCounting passedEnemyCounting =
                    enemyControlPointing.GetDeadEnemyCounting();

                passedEnemyCompositeCounter.AddDeadEnemyCounting(passedEnemyCounting);
            }

            return passedEnemyCompositeCounter;
        }
        public IEnemyWavesCounting GetEnemyWavesCounting()
        {
            EnemyWavesCompositeCounter passedEnemyCompositeCounter
                = new EnemyWavesCompositeCounter();

            foreach (IEnemyControlPointing enemyControlPointing in _enemyControlPoints)
            {
                IEnemyWavesCounting passedEnemyCounting =
                    enemyControlPointing.GetEnemyWavesCounting();

                passedEnemyCompositeCounter.AddEnemyWavesCounter(passedEnemyCounting);
            }

            return passedEnemyCompositeCounter;
        }

        #endregion
    }
}
