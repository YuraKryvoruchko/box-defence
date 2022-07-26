using System;
using UnityEngine;

using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

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
            _inputSystem.Touch.TouchPosition.performed += (ctx) => TryGetCell(ctx);
        }

        #endregion

        #region Private Methods

        private void TryGetCell(CallbackContext callbackContext)
        {
            Vector2 tapPosition = callbackContext.ReadValue<Vector2>();

            Vector3 startPosition = _mainCamera.ScreenToWorldPoint(tapPosition);

            Ray2D ray = new Ray2D(startPosition, Vector3.forward);

            Physics2D.queriesHitTriggers = false;
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, RAY_DISTANCE);
            Physics2D.queriesHitTriggers = true;

            if (hit.transform.TryGetComponent(out Cell cell))
                ClickingOnCell?.Invoke(cell);
            else
                Debug.LogWarning("Hit don't is cell");
        }

        #endregion
    }
}
