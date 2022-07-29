using System;
using UnityEngine;
using BoxDefence.Towers;

namespace BoxDefence
{
    public class TowerPicker
    {
        #region Fields

        private Tower _currentTower;

        #endregion

        #region Properties

        public Tower Tower { get => _currentTower; }

        #endregion

        #region Ñonstructor

        public TowerPicker()
        {
            ReturedTower.OnPickTower += ChooseTower;
        }

        #endregion

        #region Public Methods

        public bool IsTowerSelected()
        {
            if (_currentTower != null)
            {
                return true;
            }
            else
            {
                Debug.LogWarning("Tower not selected");

                return false;
            }
        }

        #endregion

        #region Private Methonds

        private void ChooseTower(Tower tower)
        {
            try
            {
                if (tower != null)
                    _currentTower = tower;
                else
                    throw new Exception("Picker tower is null!");
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        #endregion
    }
}
