using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace BoxDefence.AI
{
    public class Enemy : MonoBehaviour, IWalking
    {
        [Header("�haracteristics")]
        [SerializeField] private float _damage = 100f;
        [Space]
        [SerializeField] private float _speed = 1f;
        [Space]
        [SerializeField] private Vector3 _offset = new Vector3(0.5f, 0.5f, 0);

        private List<Transform> _wayPoints;

        private Vector3 _currentOffset = Vector3.zero;
        private Vector3 _pointPosition;

        private int _indexPoint = -1;

        public event Action<Enemy> OnDead;

        public static event Action OnLastPoint;

        private void Update()
        {
            if (transform.position == _pointPosition)
            {
                if (IsLastPoint() == false)
                    ChangePoint();
                else
                    Destroy();
            }  

            WalkToPoint();
        }

        public void TakeDamage(float damage)
        {
            _damage -= damage;

            if (_damage <= 0)
            {
                OnDead?.Invoke(this);

                OnDead = null;

                Destroy(gameObject);
            }
        }
        public void Init(List<Transform> wayPoints)
        {
            _currentOffset = GetAndCreateRandomOffset();

            ChangeWayPoints(wayPoints);
            ChangePoint();
            WalkToPoint();
        }
        public void ChangeWayPoints(List<Transform> wayPoints)
        {
            _wayPoints = wayPoints;
        }

        private void WalkToPoint()
        {
            float step = _speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, _pointPosition, step);
        }
        private void ChangePoint()
        {
            _indexPoint++;

            _pointPosition = _wayPoints[_indexPoint].position + _currentOffset;
        }
        private void Destroy()
        {
            OnDead?.Invoke(this);
            OnLastPoint?.Invoke();

            Destroy(gameObject);
        }
        private bool IsLastPoint()
        {
            if (_indexPoint == _wayPoints.Count - 1)
                return true;
            else
                return false;
        }
        private Vector3 GetAndCreateRandomOffset()
        {
            float xPosition = Random.Range(-_offset.x, _offset.x);
            float yPosition = Random.Range(-_offset.y, _offset.y);

            Vector3 offset = new Vector3(xPosition, yPosition);

            return offset;
        }
    }
}
