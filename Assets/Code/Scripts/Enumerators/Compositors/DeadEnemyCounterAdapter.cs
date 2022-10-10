using System;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

namespace BoxDefence.Enumerating
{
    public interface IDeadEnemyCounterAdapting : IDeadEnemyCounting
    {
        void Init();
    }
    public class DeadEnemyCounterAdapter : MonoBehaviour, IDeadEnemyCounterAdapting
    {
        #region Fields

        [SerializeField]
        private InterfaceReference<IDeadEnemyCounterGetting, MonoBehaviour>
            _deadEnemyCounterGetting;

        private IDeadEnemyCounting _deadEnemyCounter;

        #endregion

        #region Properties

        private IDeadEnemyCounterGetting DeadEnemyCounterGetting { get => 
                _deadEnemyCounterGetting.Value; }

        #endregion

        #region Actions

        public event Action OnAddDeadEnemy;
        public event Action OnRemoveDeadEnemy;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            if (_deadEnemyCounter == null)
                return;

            _deadEnemyCounter.OnAddDeadEnemy += CallEventOnAddDeadEnemy;
            _deadEnemyCounter.OnRemoveDeadEnemy += CallEventOnRemoveDeadEnemy;
        }
        private void OnDisable()
        {
            if (_deadEnemyCounter == null)
                return;

            _deadEnemyCounter.OnAddDeadEnemy -= CallEventOnAddDeadEnemy;
            _deadEnemyCounter.OnRemoveDeadEnemy -= CallEventOnRemoveDeadEnemy;
        }

        #endregion

        #region Public Methods

        public void Init()
        {
            _deadEnemyCounter = DeadEnemyCounterGetting.GetDeadEnemyCounting();
            _deadEnemyCounter.OnAddDeadEnemy += CallEventOnAddDeadEnemy;
            _deadEnemyCounter.OnRemoveDeadEnemy += CallEventOnRemoveDeadEnemy;
        }
        public int GetDeadEnemyCount()
        {
            return _deadEnemyCounter.GetDeadEnemyCount();
        }
        public int GetMaxDeadEnemyCount()
        {
            return _deadEnemyCounter.GetMaxDeadEnemyCount();
        }

        #endregion

        #region Private Methods

        private void CallEventOnAddDeadEnemy()
        {
            OnAddDeadEnemy?.Invoke();
        }
        private void CallEventOnRemoveDeadEnemy()
        {
            OnRemoveDeadEnemy?.Invoke();
        }

        #endregion
    }
}
