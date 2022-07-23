using UnityEngine;

using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace BoxDefence.CameraSystem
{
    [RequireComponent(typeof(Camera))]
    public class CameraMover : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _speedChengTransform;

        private Camera _camera;

        private InputSystem _inputActions;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            _inputActions = new InputSystem();

            _camera = GetComponent<Camera>();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Touch.TouchDelta.performed += (exp) => CameraDrag(exp);
        }
        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Touch.TouchDelta.performed -= (exp) => CameraDrag(exp);
        }

        #endregion

        #region PrivateMethods

        private void CameraDrag(CallbackContext callbackContext)
        {
            Vector2 deltaPosition = callbackContext.ReadValue<Vector2>();

            _camera.transform.position -= new Vector3(deltaPosition.x * _speedChengTransform,
                                                      deltaPosition.y * _speedChengTransform);
        }

        #endregion
    }
}
