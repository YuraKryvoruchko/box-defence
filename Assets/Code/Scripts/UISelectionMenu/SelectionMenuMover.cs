using UnityEngine;
using BoxDefence.CameraSystem;

namespace BoxDefence.UI
{
    public class SelectionMenuMover : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _currentCamera;
        [SerializeField] private CameraMover _cameraMover;
        [Space]
        [SerializeField] private SelectionMenu _selectionMenu;

        private Cell _currentCell;

        private CellFinder _cellFinder;

        private InputSystem _inputSystem;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _inputSystem = new InputSystem();

            _cellFinder = new CellFinder(_inputSystem, _currentCamera);
            _cellFinder.ClickingOnCell += OnClickOnCell;
        }
        private void OnEnable()
        {
            _inputSystem.Enable();
            _cameraMover.OnMoveCamera += OnMoveCamera;
        }
        private void OnDisable()
        {
            _inputSystem.Disable();
            _cameraMover.OnMoveCamera -= OnMoveCamera;
        }

        #endregion

        #region Private Methods

        private void OnClickOnCell(Cell cell)
        {
            if (cell == _currentCell)
            {
                DisableMenu();

                return;
            }

            _currentCell = cell;
            _selectionMenu.EnableMenu();

            Vector3 cellPosition = cell.transform.position;
            MoveSelectionMenu(cellPosition);
        }
        private void OnMoveCamera()
        {
            if (_currentCell == null)
                return;
            if (_selectionMenu.GetActivedStatusMenu() == false)
                return;

            Vector3 cellPosition = _currentCell.transform.position;
            MoveSelectionMenu(cellPosition);
        }
        private void MoveSelectionMenu(Vector3 cellPosition)
        {
            Vector2 cellRectPosition = RectTransformUtility.WorldToScreenPoint(_currentCamera, cellPosition);

            _selectionMenu.MoveToCell(cellRectPosition);
        }
        private void DisableMenu()
        {
            _selectionMenu.DisableMenu();

            _currentCell = null;
        }

        #endregion
    }
}
