using System;

namespace BoxDefence
{
    public class DeadEnemyCounter : ObjectCounter, IDeadEnemyCounting
    {
        #region Actions

        public event Action OnAddDeadEnemy;
        public event Action OnRemoveDeadEnemy;

        #endregion

        #region Counstructor

        public DeadEnemyCounter(int maxWavesCount) : base(maxWavesCount)
        {
        }
        public DeadEnemyCounter(int maxWavesCount, int currentWavesCount) : base(maxWavesCount, currentWavesCount)
        {
            for (int i = 0; i < currentWavesCount; i++)
                base.AddObject();
        }

        #endregion

        #region Public Methods

        public override void AddObject()
        {
            base.AddObject();

            OnAddDeadEnemy?.Invoke();
        }
        public override void RemoveObject()
        {
            base.RemoveObject();

            OnRemoveDeadEnemy?.Invoke();
        }
        public int GetDeadEnemyCount()
        {
            return base.GetCount();
        }
        public int GetMaxDeadEnemyCount()
        {
            return base.GetMaxCount();
        }

        #endregion
    }
}
