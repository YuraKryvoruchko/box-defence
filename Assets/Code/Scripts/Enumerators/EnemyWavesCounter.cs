using System;

namespace BoxDefence
{
    public class EnemyWavesCounter : ObjectCounter, IEnemyWavesCounting
    {
        #region Action

        public event Action OnAddEnemyWaves;
        public event Action OnRemoveEnemyWaves;

        #endregion

        #region Counstructor

        public EnemyWavesCounter(int maxWavesCount) : base(maxWavesCount)
        {
        }
        public EnemyWavesCounter(int maxWavesCount, int currentWavesCount) : base(maxWavesCount)
        {
            for (int i = 0; i < currentWavesCount; i++)
                base.AddObject();
        }

        #endregion

        #region Public Methods

        public void AddWaves()
        {
            base.AddObject();

            OnAddEnemyWaves?.Invoke();
        }
        public void RemoveWaves()
        {
            base.RemoveObject();

            OnRemoveEnemyWaves?.Invoke();
        }

        public int GetEnemyWavesCount()
        {
            return base.GetCount();
        }
        public int GetMaxEnemyWavesCount()
        {
            return base.GetMaxCount();
        }

        #endregion
    }
}
