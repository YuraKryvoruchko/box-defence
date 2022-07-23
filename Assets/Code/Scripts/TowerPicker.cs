using UnityEngine;
using UnityEngine.InputSystem;
using BoxDefence.Towers;

namespace BoxDefence
{
    public class TowerPicker : MonoBehaviour
    {
        [SerializeField] private Tower _currentTower;
        [Space]
        [SerializeField] private float _speedChengTransform = 0.05f;

        private Vector3 _oldCameraPosition;

        private InputSystem _inputActions;

        private void Awake()
        {
            _inputActions = new InputSystem();
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            ReturedTower.OnPickTower += ChooseTower;
        }
        private void OnDisable()
        {
            _inputActions.Disable();

            ReturedTower.OnPickTower -= ChooseTower;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
                SetTower();
        }

        private void SetTower()
        {
            Debug.Log("OK");

            Touch touch = Input.GetTouch(0);

            Ray2D ray = new Ray2D(UnityEngine.Camera.main.ScreenToWorldPoint(touch.position), Vector3.forward);

            Physics2D.queriesHitTriggers = false;
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100);
            Physics2D.queriesHitTriggers = true;

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out Cell cell))
                    cell.SetTower(_currentTower);
            }
        }

        private void ChooseTower(Tower tower)
        {
            if (tower != null)
                _currentTower = tower;
            else
                Debug.LogError("Picker tower is null!");
        }
    }
}
