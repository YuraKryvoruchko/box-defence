using System;
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

        private IPassedEnemyCounting _passedEnemyCounter;

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
            if (_passedEnemyCounter == null)
                return;

            _passedEnemyCounter.OnAddPassedEnemy += CallEventOnAddPassedEnemy;
            _passedEnemyCounter.OnRemovePassedEnemy += CallEventOnRemovePassedEnemy;
        }
        private void OnDisable()
        {
            if (_passedEnemyCounter == null)
                return;

            _passedEnemyCounter.OnAddPassedEnemy -= CallEventOnAddPassedEnemy;
            _passedEnemyCounter.OnRemovePassedEnemy -= CallEventOnRemovePassedEnemy;
        }

        #endregion

        #region Public Methods

        public void Init()
        {
            _passedEnemyCounter = PassedEnemyCompositeCounter.GetPassedEnemyCounting();
            _passedEnemyCounter.OnAddPassedEnemy += CallEventOnAddPassedEnemy;
            _passedEnemyCounter.OnRemovePassedEnemy += CallEventOnRemovePassedEnemy;
        }
        public int GetPassedEnemyCount()
        {
            return _passedEnemyCounter.GetPassedEnemyCount();
        }
        public int GetMaxPassedEnemyCount()
        {
            return _passedEnemyCounter.GetMaxPassedEnemyCount();
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
