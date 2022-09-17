﻿using UnityEngine;
using UnityEngine.UI;
using AYellowpaper;

namespace BoxDefence.UI
{
    public class WavesCountDisplay : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _text;
        [Space]
        [SerializeField] private InterfaceReference<IEnemyWavesCounting, MonoBehaviour> _wavesCounter;

        private int _currentWavesCount = 0;
        private int _maxWavesCount = 0;

        #endregion

        #region Properites

        private IEnemyWavesCounting WavesCounter { get => _wavesCounter.Value; }

        #endregion

        #region Unity Methods

        private void Start()
        {
            UpdateText();
        }
        private void OnEnable()
        {
            WavesCounter.OnAddEnemyWaves += UpdateText;
        }
        private void OnDisable()
        {
            WavesCounter.OnAddEnemyWaves -= UpdateText;
        }

        #endregion

        #region Private Methods

        private void UpdateText()
        {
            _currentWavesCount = WavesCounter.GetEnemyWavesCount();
            _maxWavesCount = WavesCounter.GetMaxEnemyWavesCount();

            _text.text = _currentWavesCount.ToString() 
                         + " / " 
                         + _maxWavesCount.ToString() 
                         + " Waves";
        }

        #endregion
    }
}
