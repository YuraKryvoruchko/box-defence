using UnityEngine;
using BoxDefence.AI;

using Random = UnityEngine.Random;

namespace BoxDefence.Towers
{
    public class TowerCollisionHandling : ITowerCollisionHandling
    {
        private ILogicTowerAdapter _logicTowerAdapter;

        public TowerCollisionHandling(ILogicTowerAdapter logicTowerAdapter)
        {
            _logicTowerAdapter = logicTowerAdapter;
        }

        public void OnEnter(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                _logicTowerAdapter.EnemysInShootZone.Add(enemy);

                if (_logicTowerAdapter.CurrentEnemy == null)
                    _logicTowerAdapter.CurrentEnemy = enemy;
            }
        }
        public void OnExit(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                _logicTowerAdapter.EnemysInShootZone.Remove(enemy);

                if (enemy == _logicTowerAdapter.CurrentEnemy)
                    _logicTowerAdapter.CurrentEnemy = ChooseAnEnemy();
            }
        }

        private Enemy ChooseAnEnemy()
        {
            if (_logicTowerAdapter.EnemysInShootZone.Count == 0)
                return default;

            int index = Random.Range(0, _logicTowerAdapter.EnemysInShootZone.Count);
            if (index == _logicTowerAdapter.EnemysInShootZone.Count)
                index = _logicTowerAdapter.EnemysInShootZone.Count - 1;

            Enemy enemy = _logicTowerAdapter.EnemysInShootZone[index];

            return enemy;
        }
    }
}
