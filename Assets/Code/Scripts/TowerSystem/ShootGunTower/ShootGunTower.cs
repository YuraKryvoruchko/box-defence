using BoxDefence.AI;
using BoxDefence.DamageSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoxDefence.Towers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ShootGunTower : ImprovingTower<ShootGunTower.ShootGunTowerLevel>, ILogicTowerAdapter,
        IShooterTowerAdapter, ITowerCharacteristic<ShootGunTower.ShootGunTowerLevel>
    {
        #region Fields

        [Space]
        [SerializeField] private List<Enemy> _enemysInShootZone;
        [Header("Ñharacteristics")]
        [SerializeField] private float _shootRate = 1f;
        [SerializeField] private float _colliderRadius = 3f;
        [SerializeField] private Damage _damage;
        [Header("Other")]
        [SerializeField] private GameObject _upperTower;

        [SerializeField] private Bullet _bulletPrefab;

        [SerializeField] private Transform _spawnBulletPoint;
        [Header("Tower Levels")]
        [SerializeField] private ShootGunTowerLevel[] _shootGunTowerLevels;

        private CircleCollider2D _circleCollider2D;

        private TowerShooter _towerShooter;
        private TowerCollisionHandling _towerCollisionHandling;
        private TowerWathcer _towerWathcer;

        #endregion

        #region Properties

        public Enemy CurrentEnemy { get; set; }

        public List<Enemy> EnemysInShootZone { get => _enemysInShootZone; set => _enemysInShootZone = value; }

        public IDamager Damage { get => _damage; }
        public float ShootRate { get => _shootRate; }

        public Bullet BulletPrefab { get => _bulletPrefab; }

        public Transform SpawnPoint { get => _spawnBulletPoint; }

        public override ShootGunTowerLevel[] Levels { get => _shootGunTowerLevels; }
        protected override TowerImrpover<ShootGunTowerLevel> TowerImrpover { get; set; }

        #endregion

        #region ShootGunTowerLevel

        [Serializable]
        public class ShootGunTowerLevel : ITowerImprovement
        {
            [field: SerializeField] public int Index { get; private set; }
            [field: Space]
            [field: SerializeField] public int PriceImprovement { get; private set; }
            [field: Space]
            [field: SerializeField] public float ShootRate { get; private set; }
            [field: SerializeField] public float ColliderRadius { get; private set; }
            [field: SerializeField] public Damage Damage { get; private set; }
            [field: Space]
            [field: SerializeField] public Sprite TowerFoundation { get; private set; }
            [field: SerializeField] public Sprite UpperTower { get; private set; }
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            _towerShooter = new TowerShooter(this);
            _towerCollisionHandling = new TowerCollisionHandling(this);
            _towerWathcer = new TowerWathcer(_upperTower.transform);
            TowerImrpover = new TowerImrpover<ShootGunTowerLevel>(this);

            _circleCollider2D = GetComponent<CircleCollider2D>();
            _circleCollider2D.radius = _colliderRadius;
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
            if (CurrentEnemy == null)
                return;

            if (_towerShooter.CanShoot() == true)
                _towerShooter.Shoot(CurrentEnemy);

            _towerWathcer.WatchTheEnemy(transform, CurrentEnemy.transform.position);
        }

        #endregion

        #region Public Methods

        public override void SetLevelCharacteristics(ShootGunTowerLevel levelCharacteristic)
        {
            _damage = levelCharacteristic.Damage;
            _shootRate = levelCharacteristic.ShootRate;
            _colliderRadius = levelCharacteristic.ColliderRadius;
            _circleCollider2D.radius = _colliderRadius;
            GetComponent<SpriteRenderer>().sprite = levelCharacteristic.TowerFoundation;
            _upperTower.GetComponent<SpriteRenderer>().sprite = levelCharacteristic.UpperTower;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
