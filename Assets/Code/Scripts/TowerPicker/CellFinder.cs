using System;
using UnityEngine;

namespace BoxDefence
{
    public class CellFinder
    {
        #region Fields

        private Camera _mainCamera;

        private InputSystem _inputSystem;

        private const int RAY_DISTANCE = 100;

        #endregion

        #region Actions

        public event Action<Cell> ClickingOnCell;

        #endregion

        #region Сonstructor

        public CellFinder(InputSystem inputSystem, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _inputSystem = inputSystem;
            _inputSystem.Touch.TouchTap.performed += context => TryGetCell();
        }

        #endregion

        #region Private Methods

        private void TryGetCell()
        {
            Vector2 tapPosition = _inputSystem.Touch.TouchPosition.ReadValue<Vector2>();

            Vector3 startPosition = _mainCamera.ScreenToWorldPoint(tapPosition);

            Ray2D ray = new Ray2D(startPosition, Vector3.forward);

            Physics2D.queriesHitTriggers = false;
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, RAY_DISTANCE);
            Physics2D.queriesHitTriggers = true;

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out Cell cell) == true)
                    ClickingOnCell?.Invoke(cell);
                else
                    Debug.LogWarning("Hit don't is cell");
            }
            else
            {
                Debug.LogWarning("Tap in emptiness");
            }
        }

        #endregion
    }
}
