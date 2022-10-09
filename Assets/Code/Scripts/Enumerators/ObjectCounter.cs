using System;

namespace BoxDefence
{
    public class ObjectCounter
    {
        #region Fields

        private int _count = 0;

        private int _maxCount = 0;

        private const int MIN_COUNT = 0;

        #endregion

        #region Constructor

        public ObjectCounter(int maxWavesCount)
        {
            _maxCount = maxWavesCount;
        }
        public ObjectCounter(int maxWavesCount, int currentWavesCount)
        {
            _maxCount = maxWavesCount;
            _count = currentWavesCount;
        }

        #endregion

        #region Public Methods

        public virtual void AddObject()
        {
            if (_count + 1 > _maxCount)
                throw new Exception("Current waves count > max waves count");

            _count++;
        }
        public virtual void RemoveObject()
        {
            if (_count - 1 < MIN_COUNT)
                throw new Exception("_enemyWaves < min waves count");

            _count--;
        }
        public int GetCount()
        {
            return _count;
        }
        public int GetMaxCount()
        {
            return _maxCount;
        }

        #endregion
    }
}
