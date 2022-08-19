using System;
using UnityEngine;

using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace BoxDefence.CameraSystem
{
    [RequireComponent(typeof(Camera))]
    public class CameraMover : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _speedChengTransform;
        [Space]
        [SerializeField] private CameraZoomer _cameraZoomer;

        private bool _cameraZooming = false;

        private Camera _camera;

        private InputSystem _inputActions;

        #endregion

        #region Actions

        public event Action OnMoveCamera;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            _inputActions = new InputSystem();
            _inputActions.Touch.TouchDelta.performed += (exp) => CameraDrag(exp);

            _camera = GetComponent<Camera>();
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            if (_cameraZoomer != null)
            {
                _cameraZoomer.OnStartZoomCamera += OnStartZoomCamera;
                _cameraZoomer.OnEndZoomCamera += OnEndZoomCamera;
            }
            else
            {
                Debug.LogWarning("CameraZoomer is null!");
            }
        }
        private void OnDisable()
        {
            _inputActions.Disable();

            if (_cameraZoomer != null)
            {
                _cameraZoomer.OnStartZoomCamera -= OnStartZoomCamera;
                _cameraZoomer.OnEndZoomCamera -= OnEndZoomCamera;
            }
            else
            {
                Debug.LogWarning("CameraZoomer is null!");
            }
        }

        #endregion

        #region PrivateMethods

        private void CameraDrag(CallbackContext callbackContext)
        {
            if (_cameraZooming == true)
                return;

            Vector2 deltaPosition = callbackContext.ReadValue<Vector2>();

            _camera.transform.position -= new Vector3(deltaPosition.x * _speedChengTransform,
                                                      deltaPosition.y * _speedChengTransform);

            OnMoveCamera?.Invoke();
        }
        private void OnStartZoomCamera()
        {
            _cameraZooming = true;
        }
        private void OnEndZoomCamera()
        {
            _cameraZooming = false;
        }

        #endregion
    }
}
