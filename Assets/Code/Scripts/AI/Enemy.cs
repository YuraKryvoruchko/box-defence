using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace BoxDefence.AI
{
    public class Enemy : MonoBehaviour, IWalking
    {
        [Header("Ñharacteristics")]
        [SerializeField] private float _damage = 100f;
        [Space]
        [SerializeField] private float _speed = 1f;
        [Space]
        [SerializeField] private Vector3 _offset = new Vector3(0.5f, 0.5f, 0);

        private List<Vector2> _path;

        private Vector3 _currentOffset = Vector3.zero;
        private Vector3 _pointPosition;

        private int _indexPoint = 0;

        public event Action<Enemy> OnDead;

        public static event Action OnLastPoint;

        private void Update()
        {
            if (transform.position == _pointPosition)
            {
                if (IsLastPoint() == true)
                    Destroy();
                else
                    SetNextPoint();
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
        public void Init(List<Vector2> path)
        {
            ChangePath(path);

            _currentOffset = GetAndCreateRandomOffset();
            _pointPosition = (Vector3)_path[_indexPoint] + _currentOffset;
        }
        public void ChangePath(List<Vector2> path)
        {
            _path = path;
        }

        private void WalkToPoint()
        {
            float step = _speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, _pointPosition, step);
        }
        private void SetNextPoint()
        {
            _indexPoint++;
            _pointPosition = (Vector3)_path[_indexPoint] + _currentOffset;
        }
        private bool IsLastPoint()
        {
            if (_indexPoint == _path.Count - 1)
                return true;
            else
                return false;
        }
        private void Destroy()
        {
            OnDead?.Invoke(this);
            OnLastPoint?.Invoke();

            Destroy(gameObject);
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
