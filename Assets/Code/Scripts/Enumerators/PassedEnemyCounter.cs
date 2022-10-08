using System;

namespace BoxDefence
{
    public class PassedEnemyCounter : ObjectCounter, IPassedEnemyCounting
    {
        #region Actions

        public event Action OnAddPassedEnemy;
        public event Action OnRemovePassedEnemy;

        #endregion

        #region Counstructor

        public PassedEnemyCounter(int maxWavesCount) : base(maxWavesCount)
        {
        }
        public PassedEnemyCounter(int maxWavesCount, int currentWavesCount) : base(maxWavesCount, currentWavesCount)
        {
            for (int i = 0; i < currentWavesCount; i++)
                base.AddObject();
        }

        #endregion

        #region Public Methods

        public override void AddObject()
        {
            base.AddObject();

            OnAddPassedEnemy?.Invoke();
        }
        public override void RemoveObject()
        {
            base.RemoveObject();

            OnRemovePassedEnemy?.Invoke();
        }
        public int GetMaxPassedEnemyCount()
        {
            return base.GetCount();
        }
        public int GetPassedEnemyCount()
        {
            return base.GetMaxCount();
        }

        #endregion
    }
}
