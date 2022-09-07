using UnityEngine;
using UnityEngine.UI;
using BoxDefence.TimerSystem;

namespace BoxDefence.UI
{
    public class WavesCountDisplay : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _text;
        [Space]
        [SerializeField] private CompoundSpawner _compoundSpawner;

        private Timer _timer;

        private WavesCounter _wavesCounter;

        private int _currentWavesCount = 0;
        private int _maxWavesCount = 0;

        private const float DELAY_AFTER_START = 0.01f;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _timer = new Timer(DELAY_AFTER_START);
        }
        private async void Start()
        {
            await _timer.StartTimer();

            Debug.Log("Display time: " + Time.realtimeSinceStartup);
            _wavesCounter = _compoundSpawner.GetWavesCounter();
            _wavesCounter.OnAddWaves += UpdateText;

            UpdateText();
        }
        private void OnEnable()
        {
            if(_wavesCounter != null)
                _wavesCounter.OnAddWaves += UpdateText;
        }
        private void OnDisable()
        {
            if (_wavesCounter != null)
                _wavesCounter.OnAddWaves -= UpdateText;
        }

        #endregion

        #region Private Methods

        private void UpdateText()
        {
            _currentWavesCount = _wavesCounter.GetCurrentWavesCount();
            _maxWavesCount = _wavesCounter.GetMaxWavesCount();

            _text.text = _currentWavesCount.ToString() 
                         + " / " 
                         + _maxWavesCount.ToString() 
                         + " Waves";
        }

        #endregion
    }
}
