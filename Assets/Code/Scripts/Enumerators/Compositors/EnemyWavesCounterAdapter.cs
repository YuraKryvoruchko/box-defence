﻿using System;
using UnityEngine;
using AYellowpaper;

namespace BoxDefence.Enumerating
{ 
    public interface IEnemyWavesCounterAdapting : IEnemyWavesCounting
    {
        void Init();
    }
    public class EnemyWavesCounterAdapter : MonoBehaviour, IEnemyWavesCounterAdapting
    {
        #region Fields

        [SerializeField]
        private InterfaceReference<IEnemyWavesCounterGetting, MonoBehaviour> 
            _enemyWavesCounterGetting;

        private IEnemyWavesCounting _enemyWavesCounting;

        #endregion

        #region Actions

        public event Action OnAddEnemyWaves;
        public event Action OnRemoveEnemyWaves;

        #endregion

        #region Properties

        private IEnemyWavesCounterGetting EnemyWavesCounterGetting 
        { 
            get => _enemyWavesCounterGetting.Value; 
        }

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            if (_enemyWavesCounting == null)
                return;

            _enemyWavesCounting.OnAddEnemyWaves += () => OnAddEnemyWaves?.Invoke();
            _enemyWavesCounting.OnRemoveEnemyWaves += () => OnRemoveEnemyWaves?.Invoke();
        }
        private void OnDisable()
        {
            _enemyWavesCounting.OnAddEnemyWaves -= () => OnAddEnemyWaves?.Invoke();
            _enemyWavesCounting.OnRemoveEnemyWaves -= () => OnRemoveEnemyWaves?.Invoke();
        }

        #endregion

        #region Public Methods

        public void Init()
        {
            _enemyWavesCounting = EnemyWavesCounterGetting.GetEnemyWavesCounting();
            _enemyWavesCounting.OnAddEnemyWaves += () => OnAddEnemyWaves?.Invoke();
            _enemyWavesCounting.OnRemoveEnemyWaves += () => OnRemoveEnemyWaves?.Invoke();
        }
        public int GetEnemyWavesCount()
        {
            return _enemyWavesCounting.GetEnemyWavesCount();
        }
        public int GetMaxEnemyWavesCount()
        {
            return _enemyWavesCounting.GetMaxEnemyWavesCount();
        }

        #endregion
    }
}
