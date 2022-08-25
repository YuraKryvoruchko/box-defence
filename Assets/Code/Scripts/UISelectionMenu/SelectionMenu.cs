using System;
using UnityEngine;
using BoxDefence.CameraSystem;

namespace BoxDefence.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SelectionMenu : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _camera;
        [SerializeField] private CameraMover _cameraMover;

        private RectTransform _rectTransform;

        private Cell _currentCell;
        private Transform _currentCellPosition;

        private MenuMover _menuMover;

        #endregion

        #region Action

        public event Action OnEnableMenu;
        public event Action OnDisableMenu;

        #endregion

        #region Properties

        protected Cell CurrentCell { get => _currentCell; set => _currentCell = value; }

        #endregion

        #region Unity Methods

        protected void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            _menuMover = new MenuMover(_rectTransform, _camera);
        }
        protected void OnEnable()
        {
            _cameraMover.OnMoveCamera += () => _menuMover.MoveSelectionMenu(_currentCellPosition.position);
        }
        protected void OnDisable()
        {
            _cameraMover.OnMoveCamera -= () => _menuMover.MoveSelectionMenu(_currentCellPosition.position);
        }

        #endregion

        #region Public Methods

        public void MoveToCell(Cell cell)
        {
            _currentCell = cell;
            _currentCellPosition = cell.transform;

            Vector3 cellPosition = _currentCellPosition.position;

            _menuMover.MoveSelectionMenu(cellPosition);
        }
        public bool GetActivedStatusMenu()
        {
            return gameObject.activeSelf;
        }
        public void EnableMenu()
        {
            gameObject.SetActive(true);

            OnEnableMenu?.Invoke();
        }
        public void DisableMenu()
        {
            gameObject.SetActive(false);

            OnDisableMenu?.Invoke(); ;
        }

        #endregion
    }
}
