using UnityEngine;
using System;

namespace BoxDefence.UI
{
    public class DeadEnemyCompositeCounterMonoAdapter : MonoBehaviour, IDeadEnemyCounting
    {
        #region Fields

        private DeadEnemyCompositeCounter _deadEnemyCounting;

        #endregion

        #region Actions

        public event Action OnAddDeadEnemy;
        public event Action OnRemoveDeadEnemy;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _deadEnemyCounting = new DeadEnemyCompositeCounter();
        }
        private void OnEnable()
        {
            if (_deadEnemyCounting == null)
                throw new Exception("On enable object; _deadEnemyCounting is null!");

            _deadEnemyCounting.OnAddDeadEnemy += CallEventOnAddDeadEnemy;
            _deadEnemyCounting.OnRemoveDeadEnemy += CallEventOnRemoveDeadEnemy;
        }
        private void OnDisable()
        {
            if (_deadEnemyCounting == null)
                throw new Exception("On disable object; _deadEnemyCounting is null!");

            _deadEnemyCounting.OnAddDeadEnemy -= CallEventOnAddDeadEnemy;
            _deadEnemyCounting.OnRemoveDeadEnemy -= CallEventOnRemoveDeadEnemy;
        }

        #endregion

        #region Public Methods

        public void AddDeadEnemyCounting(IDeadEnemyCounting enemyCounting)
        {
            _deadEnemyCounting.AddDeadEnemyCounting(enemyCounting);
        }
        public void RemoveDeadEnemyCounting(IDeadEnemyCounting enemyCounting)
        {
            _deadEnemyCounting.RemoveDeadEnemyCounting(enemyCounting);
        }
        public int GetDeadEnemyCount()
        {
            return _deadEnemyCounting.GetDeadEnemyCount();
        }
        public int GetMaxDeadEnemyCount()
        {
            return _deadEnemyCounting.GetMaxDeadEnemyCount();
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
