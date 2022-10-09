using System;
using System.Collections.Generic;
using UnityEngine;
using BoxDefence.Tilemaps;

namespace BoxDefence
{
    public class PassedEnemyCompositeCounterAdapter : MonoBehaviour, IPassedEnemyCounting
    {
        #region Fields

        private PassedEnemyCompositeCounter _passedEnemyCompositeCounter;

        #endregion

        #region Actions

        public event Action OnAddPassedEnemy;
        public event Action OnRemovePassedEnemy;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _passedEnemyCompositeCounter = new PassedEnemyCompositeCounter();
        }
        private void OnEnable()
        {
            _passedEnemyCompositeCounter.OnAddPassedEnemy += CallEventOnAddPassedEnemy;
            _passedEnemyCompositeCounter.OnRemovePassedEnemy += CallEventOnRemovePassedEnemy;
        }
        private void OnDisable()
        {
            _passedEnemyCompositeCounter.OnAddPassedEnemy -= CallEventOnAddPassedEnemy;
            _passedEnemyCompositeCounter.OnRemovePassedEnemy -= CallEventOnRemovePassedEnemy;
        }

        #endregion

        #region Public Methods

        public void AddPassedEnemyCounting(IPassedEnemyCounting passedEnemyCounting)
        {
            _passedEnemyCompositeCounter.AddPassedEnemyCounting(passedEnemyCounting);
        }
        public void RemovePassedEnemyCounting(IPassedEnemyCounting passedEnemyCounting)
        {
            _passedEnemyCompositeCounter.RemovePassedEnemyCounting(passedEnemyCounting);
        }
        public int GetPassedEnemyCount()
        {
            return _passedEnemyCompositeCounter.GetPassedEnemyCount();
        }
        public int GetMaxPassedEnemyCount()
        {
            return _passedEnemyCompositeCounter.GetMaxPassedEnemyCount();
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
