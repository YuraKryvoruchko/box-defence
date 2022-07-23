using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxDefence.AI;

namespace BoxDefence.Towers
{
    public class ElectroTower : Tower
    {
        [Space]
        [SerializeField] private List<Enemy> _enemysInShootZone;
        [Header("Ñharacteristics")]
        [SerializeField] private float _shootRate = 1f;
        [SerializeField] private float _damage = 10f;
        [Header("Other")]
        [SerializeField] private LineRenderer _lineRenderer;

        private Enemy _currentEnemy;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                _enemysInShootZone.Add(enemy);

                if (_currentEnemy == null)
                    _currentEnemy = enemy;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                _enemysInShootZone.Remove(enemy);

                if (enemy == _currentEnemy)
                    _currentEnemy = ChooseAnEnemy();
            }
        }

        private void Update()
        {
            if (_currentEnemy != null)
                Shoot();
            else
                _currentEnemy = ChooseAnEnemy();
        }

        private void Shoot()
        {
            DrawLine();

            float damage = _damage * Time.deltaTime;

            _currentEnemy.TakeDamage(damage);
        }
        private void DrawLine()
        {
            _lineRenderer.SetPosition(0, _lineRenderer.transform.position);

            Vector3 endPosition = new Vector3(_currentEnemy.transform.position.x,
                                              _currentEnemy.transform.position.y,
                                              _lineRenderer.transform.position.z);

            _lineRenderer.SetPosition(1, endPosition);
        }

        private Enemy ChooseAnEnemy()
        {
            if (_enemysInShootZone.Count == 0)
                return default;

            int index = Random.Range(0, _enemysInShootZone.Count);
            if (index == _enemysInShootZone.Count)
                index = _enemysInShootZone.Count - 1;

            Enemy enemy = _enemysInShootZone[index];

            return enemy;
        }
    }
}
