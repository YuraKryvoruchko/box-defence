using System;
using System.Collections.Generic;

namespace BoxDefence.Enumerating
{
    public class EnemyWavesCompositeCounter : IEnemyWavesCounting
    {
        #region Fields

        private List<IEnemyWavesCounting> _enemyWavesCountings;

        #endregion

        #region Actions

        public event Action OnAddEnemyWaves;
        public event Action OnRemoveEnemyWaves;

        #endregion

        #region Constructor

        public EnemyWavesCompositeCounter()
        {
            _enemyWavesCountings = new List<IEnemyWavesCounting>();
        }

        #endregion

        #region Public Methods

        public void AddEnemyWavesCounter(IEnemyWavesCounting enemyWavesCounting)
        {
            _enemyWavesCountings.Add(enemyWavesCounting);
            enemyWavesCounting.OnAddEnemyWaves += CallEventOnAddEnemyWaves;
            enemyWavesCounting.OnRemoveEnemyWaves += CallEventOnRemoveEnemyWaves;
        }
        public void RemoveEnemyWavesCounter(IEnemyWavesCounting enemyWavesCounting)
        {
            _enemyWavesCountings.Remove(enemyWavesCounting);
            enemyWavesCounting.OnAddEnemyWaves -= CallEventOnAddEnemyWaves;
            enemyWavesCounting.OnRemoveEnemyWaves -= CallEventOnRemoveEnemyWaves;
        }
        public int GetEnemyWavesCount()
        {
            int enemyWavesCount = 0;

            foreach (IEnemyWavesCounting enemyWavesCounting in _enemyWavesCountings)
                enemyWavesCount += enemyWavesCounting.GetEnemyWavesCount();

            return enemyWavesCount;
        }
        public int GetMaxEnemyWavesCount()
        {
            int maxEnemyWavesCount = 0;

            foreach (IEnemyWavesCounting enemyWavesCounting in _enemyWavesCountings)
                maxEnemyWavesCount += enemyWavesCounting.GetMaxEnemyWavesCount();

            return maxEnemyWavesCount;
        }

        #endregion

        #region Private Methods

        private void CallEventOnAddEnemyWaves()
        {
            OnAddEnemyWaves.Invoke();
        }
        private void CallEventOnRemoveEnemyWaves()
        {
            OnRemoveEnemyWaves.Invoke();
        }

        #endregion
    }
}
