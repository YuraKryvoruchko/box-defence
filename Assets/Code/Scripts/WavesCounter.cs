using System;

namespace BoxDefence
{
    public class WavesCounter
    {
        #region Fields

        private int _currentWavesCount = 0;

        private int _maxWavesCount = 0;

        private const int MIN_WAVES_COUNT = 0;

        #endregion

        #region Actions

        public event Action OnAddWaves;
        public event Action OnDeleteWaves;

        #endregion

        #region Constructor

        public WavesCounter(int maxWavesCount)
        {
            _maxWavesCount = maxWavesCount;
        }
        public WavesCounter(int maxWavesCount, int currentWavesCount)
        {
            _maxWavesCount = maxWavesCount;
            _currentWavesCount = currentWavesCount;
        }

        #endregion

        #region Public Methods

        public void AddWavesToCount()
        {
            if (_currentWavesCount + 1 > _maxWavesCount)
                throw new Exception("Current waves count > max waves count");

            _currentWavesCount++;
            OnAddWaves?.Invoke();
        }
        public void RemoveWavesFromCount()
        {
            if (_currentWavesCount - 1 < MIN_WAVES_COUNT)
                throw new Exception("_enemyWaves < min waves count");

            _currentWavesCount--;
            OnDeleteWaves?.Invoke();
        }
        public int GetCurrentWavesCount()
        {
            return _currentWavesCount;
        }
        public int GetMaxWavesCount()
        {
            return _maxWavesCount;
        }

        #endregion
    }
}
