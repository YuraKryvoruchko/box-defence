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
        [Space]
        [SerializeField] private GameObject _menuContext;

        private RectTransform _rectTransform;

        private Cell _currentCell;
        private Transform _currentCellPosition;

        private MenuMover _menuMover;

        #endregion

        #region Actions

        public event Action OnShowMenu;
        public event Action OnHideMenu;

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
            SubscribeToCameraChanges();
        }
        protected void OnDisable()
        {
            UnsubscribeFromCameraChanges();
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
            return _menuContext.activeSelf;
        }
        public void ShowMenu()
        {
            _menuContext.SetActive(true);

            OnShowMenu?.Invoke();
        }
        public void HideMenu()
        {
            _menuContext.SetActive(false);

            OnHideMenu?.Invoke(); ;
        }

        #endregion

        #region Private Methods

        private void SubscribeToCameraChanges()
        {
            OnShowMenu += () => SubscribeOnMoveCamera();
            OnHideMenu += () => UnsubscribeFromMoveCamera();
        }
        private void UnsubscribeFromCameraChanges()
        {
            OnShowMenu -= () => SubscribeOnMoveCamera();
            OnHideMenu -= () => UnsubscribeFromMoveCamera();
        }
        private void SubscribeOnMoveCamera()
        {
            _cameraMover.OnMoveCamera += () => 
            _menuMover.MoveSelectionMenu(_currentCellPosition.position);
        }
        private void UnsubscribeFromMoveCamera()
        {
            _cameraMover.OnMoveCamera -= () =>
            _menuMover.MoveSelectionMenu(_currentCellPosition.position);
        }

        #endregion
    }
}
