using System;
using System.Collections.Generic;
using UnityEngine;
using BoxDefence.Damage;

using Random = UnityEngine.Random;

namespace BoxDefence.AI
{
    public class Enemy : MonoBehaviour, IWalking
    {
        #region Fields

        [Header("Ñharacteristics")]
        [SerializeField] private float _health = 100f;
        [Space]
        [SerializeField] private float _speed = 1f;
        [Space]
        [SerializeField] private Vector3 _offset = new Vector3(0.5f, 0.5f, 0);

        private List<Vector2> _path;

        private Vector3 _currentOffset = Vector3.zero;
        private Vector3 _pointPosition;

        private int _indexPoint = 0;

        private float _healthOnStart;

        private const int MAX_HEALTH_PERCENTAGE = 100;

        #endregion

        #region Events

        public event Action<Enemy> OnDead;
        public event Action<Enemy> OnPassed;

        #endregion

        #region Unity Methods

        protected void Awake()
        {
            _healthOnStart = _health;
        }
        protected void Update()
        {
            if (transform.position == _pointPosition)
            {
                if (IsLastPoint() == true)
                    PassedLevel();
                else
                    SetNextPoint();
            }

            WalkToPoint();
        }

        #endregion

        #region Public Methods

        public virtual void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0)
                Destroy();
        }
        public virtual void TakeDamage(IDamager damager)
        {
            float damage = damager.GetDamage();
            _health -= damage;

            if (_health <= 0)
                Destroy();
        }
        public void TreatOn(float percentageInDozens)
        {
            if (_health == _healthOnStart)
                return;

            float healthOnOnePercentage = _healthOnStart / MAX_HEALTH_PERCENTAGE;
            float addedHealth = healthOnOnePercentage * percentageInDozens;

            _health += addedHealth;
            if (_health > _healthOnStart)
                _health = _healthOnStart;
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

        #endregion

        #region Private Methods

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
        protected void Destroy()
        {
            OnDead?.Invoke(this);
            OnDead = null;

            Destroy(gameObject);
        }
        private void PassedLevel()
        {
            OnPassed?.Invoke(this);
            OnPassed = null;

            Destroy(gameObject);
        }
        private Vector3 GetAndCreateRandomOffset()
        {
            float xPosition = Random.Range(-_offset.x, _offset.x);
            float yPosition = Random.Range(-_offset.y, _offset.y);

            Vector3 offset = new Vector3(xPosition, yPosition);

            return offset;
        }

        #endregion
    }
}
