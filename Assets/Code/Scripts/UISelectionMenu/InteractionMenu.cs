using UnityEngine;
using BoxDefence.Towers;

namespace BoxDefence.UI
{
    public class InteractionMenu : SelectionMenu
    {
        private Tower _currentTower;

        private Cell _currentCell;

        private void Start()
        {
            _currentTower = _currentCell.Tower;
        }

        public void ImproveTower()
        {
            _currentTower.Improve();
        }
        public void DeleteTower()
        {
            if (_currentCell.IsTowerSet() == true)
                _currentCell.DeleteTower();
            else
                Debug.LogWarning("Tower dont set");
        }
    }
}
