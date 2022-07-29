using UnityEngine;

namespace BoxDefence
{
    public class TowerDeleter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _mainCamera;

        private CellFinder _cellFinder;

        private InputSystem _inputSystem;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _inputSystem = new InputSystem();
        }
        private void Start()
        {
            _cellFinder = new CellFinder(_inputSystem, _mainCamera);
            _cellFinder.ClickingOnCell += Delete;
        }
        private void OnEnable()
        {
            _inputSystem.Enable();
        }
        private void OnDisable()
        {
            _inputSystem.Disable();
        }

        #endregion

        #region Private Methods

        private void Delete(Cell cell)
        {
            if (cell.IsTowerSet() == true)
                cell.DeleteTower();
        }

        #endregion
    }
}
