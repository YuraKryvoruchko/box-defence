using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoxDefence.AI
{
    public class Enemy : MonoBehaviour
    {
        [Header("Ñharacteristics")]
        [SerializeField] private float _damage = 100f;
        [Space]
        [SerializeField] private float _speed = 1f;

        private List<Transform> _wayPoints;

        private int _indexPoint = 0;

        public event Action OnDead;

        public static event Action OnLastPoint;

        private void Update()
        {
            if (_indexPoint < _wayPoints.Count - 1)
            {
                Vector3 targetPosition = new Vector3(_wayPoints[_indexPoint].position.x, 
                                                     _wayPoints[_indexPoint].position.y, 
                                                     -1);

                if (transform.position == targetPosition)
                    _indexPoint++;
            }
            else
            {
                Vector3 targetPosition = new Vector3(_wayPoints[_indexPoint].position.x,
                                                     _wayPoints[_indexPoint].position.y,
                                                     -1);

                if (transform.position == targetPosition)
                {
                    Debug.Log("Lost");

                    OnLastPoint?.Invoke();

                    Destroy(gameObject);
                }
            }

            WalkToPoint();
        }

        public void TakeDamage(float damage)
        {
            _damage -= damage;

            if (_damage <= 0)
            {
                OnDead?.Invoke();

                OnDead = null;

                Destroy(gameObject);
            }
        }

        public void Init(List<Transform> wayPoints)
        {
            _wayPoints = wayPoints;
        }

        private void WalkToPoint()
        {
            float step = _speed * Time.deltaTime;

            Vector3 pointPosition = new Vector3(_wayPoints[_indexPoint].position.x, 
                                                _wayPoints[_indexPoint].position.y, 
                                                transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, pointPosition, step);
        }
    }
}
