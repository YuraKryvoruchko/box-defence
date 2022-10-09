using System;
using System.Collections.Generic;

namespace BoxDefence
{
    public class DeadEnemyCompositeCounter : IDeadEnemyCounting
    {
        #region Fields

        private List<IDeadEnemyCounting> _deadEnemyCountings;

        #endregion

        #region Actions

        public event Action OnAddDeadEnemy;
        public event Action OnRemoveDeadEnemy;

        #endregion

        #region Constructor

        public DeadEnemyCompositeCounter()
        {
            _deadEnemyCountings = new List<IDeadEnemyCounting>();
        }

        #endregion

        #region Public Methods

        public void AddDeadEnemyCounting(IDeadEnemyCounting enemyCounting)
        {
            _deadEnemyCountings.Add(enemyCounting);
            enemyCounting.OnAddDeadEnemy += CallEventOnAddDeadEnemy;
            enemyCounting.OnRemoveDeadEnemy += CallEventOnRemoveDeadEnemy;
        }
        public void RemoveDeadEnemyCounting(IDeadEnemyCounting enemyCounting)
        {
            _deadEnemyCountings.Remove(enemyCounting);
            enemyCounting.OnAddDeadEnemy -= CallEventOnAddDeadEnemy;
            enemyCounting.OnRemoveDeadEnemy -= CallEventOnRemoveDeadEnemy;
        }
        public int GetDeadEnemyCount()
        {
            int deadEnemyCount = 0;

            foreach (IDeadEnemyCounting deadEnemyCounting in _deadEnemyCountings)
                deadEnemyCount += deadEnemyCounting.GetDeadEnemyCount();

            return deadEnemyCount;
        }
        public int GetMaxDeadEnemyCount()
        {
            int maxDeadEnemyCount = 0;

            foreach (IDeadEnemyCounting deadEnemyCounting in _deadEnemyCountings)
                maxDeadEnemyCount += deadEnemyCounting.GetMaxDeadEnemyCount();

            return maxDeadEnemyCount;
        }

        #endregion

        #region Private Methods

        private void CallEventOnAddDeadEnemy()
        {
            OnAddDeadEnemy.Invoke();
        }
        private void CallEventOnRemoveDeadEnemy()
        {
            OnRemoveDeadEnemy.Invoke();
        }

        #endregion
    }
}
