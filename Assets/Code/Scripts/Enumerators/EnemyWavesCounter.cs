using System;

namespace BoxDefence
{
    public class EnemyWavesCounter : WavesCounter, IEnemyWavesCounting
    {
        #region Counstructor

        public EnemyWavesCounter(int maxWavesCount) : base(maxWavesCount)
        {
        }
        public EnemyWavesCounter(int maxWavesCount, int currentWavesCount) : base(maxWavesCount)
        {
            for (int i = 0; i < currentWavesCount; i++)
                base.AddWavesToCount();
        }

        #endregion

        #region Action

        public event Action OnAddEnemyWaves;
        public event Action OnRemoveEnemyWaves;

        #endregion

        #region Public Methods

        public void AddWaves()
        {
            base.AddWavesToCount();

            OnAddEnemyWaves?.Invoke();
        }
        public void RemoveWaves()
        {
            base.RemoveWavesFromCount();

            OnRemoveEnemyWaves?.Invoke();
        }

        public int GetEnemyWavesCount()
        {
            return base.GetCurrentWavesCount();
        }
        public int GetMaxEnemyWavesCount()
        {
            return base.GetMaxWavesCount();
        }

        #endregion
    }
}
