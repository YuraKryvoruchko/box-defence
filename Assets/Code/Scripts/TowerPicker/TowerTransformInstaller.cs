using UnityEngine;

namespace BoxDefence
{
    public class TowerTransformInstaller : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _mainCamera;

        private TowerPicker _towerPicker;
        private CellFinder _cellFinder;

        private InputSystem _inputSystem;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _inputSystem = new InputSystem();
        }
        private void OnEnable()
        {
            _inputSystem.Enable();
        }
        private void OnDisable()
        {
            _inputSystem.Disable();
        }
        private void Start()
        {
            _towerPicker = new TowerPicker();
            _cellFinder = new CellFinder(_inputSystem, _mainCamera);
            _cellFinder.ClickingOnCell += SetTower;
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
