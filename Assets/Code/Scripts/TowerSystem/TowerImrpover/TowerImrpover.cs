using System;

namespace BoxDefence.Towers
{
    public class TowerImrpover<TowerLevelType>
    {
        #region Fields

        private ITowerCharacteristic<TowerLevelType> _tower;

        private TowerLevelType[] _characteristicLevels;

        private int _levelIndex;

        #endregion

        #region Constructor

        public TowerImrpover(ITowerCharacteristic<TowerLevelType> characteristic)
        {
            _tower = characteristic;
            _characteristicLevels = _tower.Levels;
        }

        #endregion

        #region Public Methods

        public void ImproveToNextLevel()
        {
            if (_levelIndex + 1 >= _characteristicLevels.Length)
                throw new Exception();

            _levelIndex++;

            TowerLevelType characteristic = _characteristicLevels[_levelIndex];

            SetCharacteristic(characteristic);
        }
        public TowerLevelType GetCurrentLevel()
        {
            return _characteristicLevels[_levelIndex];
        }
        public TowerLevelType GetNextLevel()
        {
            if (_levelIndex + 1 >= _characteristicLevels.Length)
                throw new Exception();

            return _characteristicLevels[_levelIndex + 1];
        }
        public bool IsMaxLevel()
        {
            if (_levelIndex < _characteristicLevels.Length - 1)
                return false;
            else
                return true;
        }

        #endregion

        #region Private Methods

        private void SetCharacteristic(TowerLevelType characteristic)
        {
            _tower.SetLevelCharacteristics(characteristic);
        }

        #endregion
    }
}
