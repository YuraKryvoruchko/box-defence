using System.Collections.Generic;
using UnityEngine;
using BoxDefence.AI;

namespace BoxDefence.Towers
{
    public class ShootGunTower : Tower, ILogicTowerAdapter, IShooterTowerAdapter
    {
        [Space]
        [SerializeField] private List<Enemy> _enemysInShootZone;
        [Header("Ñharacteristics")]
        [SerializeField] private float _shootRate = 1f;
        [SerializeField] private float _damage = 10f;
        [Header("Other")]
        [SerializeField] private GameObject _upperTower;

        [SerializeField] private Bullet _bulletPrefab;

        [SerializeField] private Transform _spawnBulletPoint;

        private TowerShooter _towerShooter;
        private TowerCollisionHandling _towerCollisionHandling;
        private TowerWathcer _towerWathcer;

        #region Properties
        public Enemy CurrentEnemy { get; set; }

        public List<Enemy> EnemysInShootZone { get => _enemysInShootZone; set => _enemysInShootZone = value; }

        public float Damage { get => _damage; }
        public float ShootRate { get => _shootRate; }

        public Bullet BulletPrefab { get => _bulletPrefab; }

        public Transform SpawnPoint { get => _spawnBulletPoint; }

        #endregion

        #region UnityMethods

        private void Start()
        {
            _towerShooter = new TowerShooter(this);
            _towerCollisionHandling = new TowerCollisionHandling(this);
            _towerWathcer = new TowerWathcer(_upperTower.transform);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _towerCollisionHandling.OnEnter(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            _towerCollisionHandling.OnExit(collision);
        }

        private void Update()
        {
            if (CurrentEnemy != null)
            {
                if(_towerShooter.CanShoot() == true)
                    _towerShooter.Shoot(CurrentEnemy);

                _towerWathcer.WatchTheEnemy(transform, CurrentEnemy.transform.position);
            }
        }

        #endregion
    }
}
