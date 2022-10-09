using System;
using System.Collections.Generic;

namespace BoxDefence
{
    public class PassedEnemyCompositeCounter : IPassedEnemyCounting
    {
        #region Fields

        private List<IPassedEnemyCounting> _passedEnemyCountings;

        #endregion

        #region Actions

        public event Action OnAddPassedEnemy;
        public event Action OnRemovePassedEnemy;

        #endregion

        #region Constructor

        public PassedEnemyCompositeCounter()
        {
            _passedEnemyCountings = new List<IPassedEnemyCounting>();
        }

        #endregion

        #region Public Methods

        public void AddPassedEnemyCounting(IPassedEnemyCounting passedEnemyCounting)
        {
            _passedEnemyCountings.Add(passedEnemyCounting);
            passedEnemyCounting.OnAddPassedEnemy += CallEventOnAddPassedEnemy;
            passedEnemyCounting.OnRemovePassedEnemy += CallEventOnRemovePassedEnemy;
        }
        public void RemovePassedEnemyCounting(IPassedEnemyCounting passedEnemyCounting)
        {
            _passedEnemyCountings.Remove(passedEnemyCounting);
            passedEnemyCounting.OnAddPassedEnemy -= CallEventOnAddPassedEnemy;
            passedEnemyCounting.OnRemovePassedEnemy -= CallEventOnRemovePassedEnemy;
        }
        public int GetPassedEnemyCount()
        {
            int passedEnemyCount = 0;

            foreach (IPassedEnemyCounting passedEnemyCounting in _passedEnemyCountings)
                passedEnemyCount += passedEnemyCounting.GetPassedEnemyCount();

            return passedEnemyCount;
        }
        public int GetMaxPassedEnemyCount()
        {
            int maxPassedEnemyCount = 0;

            foreach (IPassedEnemyCounting passedEnemyCounting in _passedEnemyCountings)
                maxPassedEnemyCount += passedEnemyCounting.GetMaxPassedEnemyCount();

            return maxPassedEnemyCount;
        }

        #endregion

        #region Private Methods

        private void CallEventOnAddPassedEnemy()
        {
            OnAddPassedEnemy.Invoke();
        }
        private void CallEventOnRemovePassedEnemy()
        {
            OnRemovePassedEnemy.Invoke();
        }

        #endregion
    }
}
