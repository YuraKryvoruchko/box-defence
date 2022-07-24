using UnityEngine;

namespace BoxDefence
{
    public class TowerTransformInstaller : MonoBehaviour
    {
        #region Fields

        private TowerPicker _towerPicker;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _towerPicker = new TowerPicker();
        }

        private void OnEnable()
        {
            Cell.OnTap += SetTower;
        }
        private void OnDisable()
        {
            Cell.OnTap -= SetTower;
        }

        #endregion

        #region Private Methods

        private void SetTower(Cell cell)
        {
            if (_towerPicker.IsTowerSelected() == true)
            {
                if (cell.CanSetTower() == true)
                    cell.SetTower(_towerPicker.Tower);
                else
                    Debug.LogWarning("Cell is empty");
            }
            else
            {
                Debug.LogWarning("Tower not selected");
            }
        }

        #endregion
    }
}
