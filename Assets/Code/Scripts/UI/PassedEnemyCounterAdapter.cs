﻿using System;
using UnityEngine;
using AYellowpaper;

namespace BoxDefence
{
    public interface IPassedEnemyCounterAdapting : IPassedEnemyCounting
    {
        void Init();
    }
    public class PassedEnemyCounterAdapter : MonoBehaviour, IPassedEnemyCounterAdapting
    {
        #region Fields

        [SerializeField]
        private InterfaceReference<IPassedEnemyCounterGetting, MonoBehaviour>
            _passedEnemyCounterGetting;

        private IPassedEnemyCounting _passedEnemyCompositeCounter;

        #endregion

        #region Propeties

        private IPassedEnemyCounterGetting PassedEnemyCompositeCounter { 
            get => _passedEnemyCounterGetting.Value; }

        #endregion

        #region Actions

        public event Action OnAddPassedEnemy;
        public event Action OnRemovePassedEnemy;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            if (_passedEnemyCompositeCounter == null)
                return;

            _passedEnemyCompositeCounter.OnAddPassedEnemy += CallEventOnAddPassedEnemy;
            _passedEnemyCompositeCounter.OnRemovePassedEnemy += CallEventOnRemovePassedEnemy;
        }
        private void OnDisable()
        {
            if (_passedEnemyCompositeCounter == null)
                return;

            _passedEnemyCompositeCounter.OnAddPassedEnemy -= CallEventOnAddPassedEnemy;
            _passedEnemyCompositeCounter.OnRemovePassedEnemy -= CallEventOnRemovePassedEnemy;
        }

        #endregion

        #region Public Methods

        public void Init()
        {
            _passedEnemyCompositeCounter = PassedEnemyCompositeCounter.GetPassedEnemyCounting();
            _passedEnemyCompositeCounter.OnAddPassedEnemy += CallEventOnAddPassedEnemy;
            _passedEnemyCompositeCounter.OnRemovePassedEnemy += CallEventOnRemovePassedEnemy;
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
