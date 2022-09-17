using UnityEngine;

namespace BoxDefence.UI
{
    public class MenuMover
    {
        #region Fields

        private Camera _currentCamera;

        private RectTransform _rectTransformMenu;

        #endregion

        #region Constructor

        public MenuMover(RectTransform rectTransformMenu, Camera camera)
        {
            _rectTransformMenu = rectTransformMenu;
            _currentCamera = camera;
        }

        #endregion

        #region Public Methods

        public void MoveSelectionMenu(Vector3 cellPosition)
        {
            Vector2 cellRectPosition = RectTransformUtility.WorldToScreenPoint(_currentCamera, cellPosition);

            _rectTransformMenu.transform.position = cellRectPosition;
        }

        #endregion
    }
}
