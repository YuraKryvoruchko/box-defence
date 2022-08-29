using UnityEngine;

namespace BoxDefence.UI
{
    public class MenuSwitcher : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TowerCreatorMenu _towerCreatorMenu;
        [SerializeField] private InteractionMenu _interactionMenu;
        [Space]
        [SerializeField] private Camera _camera;

        private SelectionMenu _currentMenu;

        private CellFinder _cellFinder;

        private Cell _currentCell;

        private InputSystem _inputSystem;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _inputSystem = new InputSystem();
            _cellFinder = new CellFinder(_inputSystem, _camera);
        }
        private void OnEnable()
        {
            _inputSystem.Enable();
            _cellFinder.ClickingOnCell += EnableMenu;
        }
        private void OnDisable()
        {
            _inputSystem.Disable();
            _cellFinder.ClickingOnCell -= EnableMenu;
        }

        #endregion

        #region Private Methods

        private void EnableMenu(Cell cell)
        {
            if (_currentCell == cell)
            {
                _currentMenu.HideMenu();
                return;
            }

            _currentCell = cell;

            if (cell.IsTowerSet() == true)
                EnableInteractionMenu();
            else
                EnableSelectionMenu();
        }
        private void OnDisableMenu()
        {
            _currentMenu.OnHideMenu -= OnDisableMenu;
            _currentMenu = null;

            _currentCell = null;
        }
        private void EnableInteractionMenu()
        {
            EnableMenu(_interactionMenu, _towerCreatorMenu);
        }
        private void EnableSelectionMenu()
        {
            EnableMenu(_towerCreatorMenu, _interactionMenu);
        }
        private void EnableMenu(SelectionMenu enabledMenu, SelectionMenu disabledMenu)
        {
            if (_currentMenu != null)
                _currentMenu.OnHideMenu -= OnDisableMenu;

            if (disabledMenu.GetActivedStatusMenu() == true)
                disabledMenu.HideMenu();

            _currentMenu = enabledMenu;
            _currentMenu.OnHideMenu += OnDisableMenu;
            _currentMenu.MoveToCell(_currentCell);
            _currentMenu.ShowMenu();
        }

        #endregion
    }
}
