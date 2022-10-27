using System;
using UnityEngine;
using BoxDefence.DamageSystem;
using BoxDefence.AI;
using System.Collections.Generic;

namespace BoxDefence.Towers
{
    public class ArtilleryTower : ImprovingTower<ArtilleryTower.ArtilleryTowerLevels>, 
        ILogicTowerAdapter, IArtilleryTowerShooting
    {
        #region Fields

        [SerializeField] private float _shootRate;
        [SerializeField] private float _enemyDetectionRadius;
        [Space]
        [SerializeField] private ArtilleryBullet _artilleryBullet;
        [SerializeField] private Damage _currentDamage;
        [SerializeField] private ArtilleryGun _artilleryGun;
        [SerializeField] private CircleCollider2D _enemyDetectionTrigger;
        [Space]
        [SerializeField] private ArtilleryTowerLevels[] _artilleryTowerLevels;

        private TowerCollisionHandling _towerCollisionHandling;

        private Sprite _currentArtilleryTowerSprite;
        private SpriteRenderer _spriteRenderer;

        private const int FIRST_LEVEL_OF_ARTILLERY_TOWER = 0;

        #endregion

        #region Properies

        public float ShootRate { get => _shootRate; }
        public ArtilleryBullet ArtilleryBullet { get => _artilleryBullet; }
        public Damage Damage { get => _currentDamage; }

        public Enemy CurrentEnemy { get; set; }
        public List<Enemy> EnemysInShootZone { get; set; }

        public override ArtilleryTowerLevels[] Levels { get => _artilleryTowerLevels; }

        protected override TowerImrpover<ArtilleryTowerLevels> TowerImrpover { get; set; }


        #endregion

        #region ArtilleryTowerLevels

        [Serializable]
        public class ArtilleryTowerLevels : ITowerImprovement
        {
            [field: SerializeField] public int Index { get; private set; }
            [field: Space]
            [field: SerializeField] public int PriceImprovement { get; private set; }
            [field: Space]
            [field: SerializeField] public float ShootRate { get; private set; }
            [field: SerializeField] public float EnemyDetectionRadius { get; private set; }
            [field: Space]
            [field: SerializeField] public Damage Damage { get; private set; }
            [field: Space]
            [field: SerializeField] public Sprite ArtilleryTowerSprite { get; private set; }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _towerCollisionHandling = new TowerCollisionHandling(this);
            _artilleryGun.Init(this);

            SetLevelCharacteristics(_artilleryTowerLevels[FIRST_LEVEL_OF_ARTILLERY_TOWER]);
        }
        private void Update()
        {
            if (CurrentEnemy == null)
                return;

            if (_artilleryGun.CanShoot() == true)
                _artilleryGun.Shoot(CurrentEnemy);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _towerCollisionHandling.OnEnter(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            _towerCollisionHandling.OnExit(collision);
        }

        #endregion

        #region Public Methods

        public override void SetLevelCharacteristics(ArtilleryTowerLevels levelCharacteristic)
        {
            _currentDamage = levelCharacteristic.Damage;
            _enemyDetectionRadius = levelCharacteristic.EnemyDetectionRadius;
            _shootRate = levelCharacteristic.ShootRate;
            _currentArtilleryTowerSprite = levelCharacteristic.ArtilleryTowerSprite;

            SetEnemyDetectionRadius(_enemyDetectionRadius);
            SetArtilleryTowerSprite(_currentArtilleryTowerSprite);
        }

        #endregion

        #region Private Methods

        private void SetEnemyDetectionRadius(float enemyDetectionRadius)
        {
            if (enemyDetectionRadius < 0)
                throw new Exception("The radius cannot be less than zero!");

            _enemyDetectionTrigger.radius = enemyDetectionRadius;
        }
        private void SetArtilleryTowerSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        #endregion
    }
}
