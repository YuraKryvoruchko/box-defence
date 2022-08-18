using System;
using System.Collections;
using UnityEngine;

namespace BoxDefence.CameraSystem
{
    public class CameraZoomer : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _speedChengZoom;
        [Space]
        [SerializeField] private float _maxCameraSize;
        [SerializeField] private float _minCameraSize;

        private Camera _camera;

        private Coroutine _zoomCamera;

        private InputSystem _inputActions;

        #endregion

        #region Actions

        public event Action OnStartZoomCamera;
        public event Action OnEndZoomCamera;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            _inputActions = new InputSystem();
            _inputActions.Touch.TouchSecondContact.performed += (exp) => ZoomStart();
            _inputActions.Touch.TouchSecondContact.canceled += (exp) => ZoomEnd();

            _camera = GetComponent<Camera>();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }
        private void OnDisable()
        {
            _inputActions.Disable();
        }

        #endregion

        #region PrivateMethods

        private void ZoomStart()
        {
            _zoomCamera = StartCoroutine(ZoomDetection());
            OnStartZoomCamera?.Invoke();
        }
        private void ZoomEnd()
        {
            StopCoroutine(_zoomCamera);
            OnEndZoomCamera?.Invoke();
        }
        private IEnumerator ZoomDetection()
        {
            float distance = 0f;
            float previousDistance = 0f;

            while (true)
            {
                Vector2 primaryPosition = _inputActions.Touch.TouchFirstPosition.ReadValue<Vector2>();
                Vector2 seconderyPosition = _inputActions.Touch.TouchSecondPosition.ReadValue<Vector2>();

                distance = Vector2.Distance(primaryPosition, seconderyPosition);
                if (previousDistance == 0)
                    previousDistance = distance;

                float deltaDistance = GetDeltaDistance(distance, previousDistance);
                float newSize = GetNewSize(deltaDistance);
                previousDistance = distance;

                if (newSize != 0)
                    SetCameraSize(newSize);

                yield return new WaitForEndOfFrame();
            }
        }
        private float GetDeltaDistance(float distance, float previousDistance)
        {
            float deltaDistance = previousDistance - distance;

            return deltaDistance;
        }
        private float GetNewSize(float deltaDistance)
        {
            float additionalSize = deltaDistance * _speedChengZoom * Time.deltaTime;
            float newSize = _camera.orthographicSize + additionalSize;

            return newSize;
        }
        private void SetCameraSize(float newSize)
        {
            if (newSize > _maxCameraSize)
                newSize = _maxCameraSize;
            else if (newSize < _minCameraSize)
                newSize = _minCameraSize;

            _camera.orthographicSize = newSize;
        }

        #endregion
    }
}
