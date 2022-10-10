using UnityEngine;
using UnityEngine.UI;
using AYellowpaper;
using BoxDefence.Enumerating;

namespace BoxDefence.UI
{
    public class WavesCountDisplay : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _text;
        [Space]
        [SerializeField] private SpawnTileObserver _spawnTileObserver;
        [SerializeField] private InterfaceReference<IEnemyWavesCounterAdapting, MonoBehaviour> 
            _wavesCounter;

        private int _currentWavesCount = 0;
        private int _maxWavesCount = 0;

        #endregion

        #region Properites

        private IEnemyWavesCounterAdapting WavesCounter { get => _wavesCounter.Value; }

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _spawnTileObserver.OnCreateAllEnemyBase += Init;

            if(WavesCounter != null)
                WavesCounter.OnAddEnemyWaves += UpdateText;
        }
        private void OnDisable()
        {
            _spawnTileObserver.OnCreateAllEnemyBase -= Init;

            if (WavesCounter != null)
                WavesCounter.OnAddEnemyWaves -= UpdateText;
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            WavesCounter.Init();
            WavesCounter.OnAddEnemyWaves += UpdateText;

            UpdateText();
        }
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
