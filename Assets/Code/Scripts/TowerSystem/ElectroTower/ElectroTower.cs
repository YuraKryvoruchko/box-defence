using System;
using System.Collections.Generic;
using UnityEngine;
using BoxDefence.AI;
using BoxDefence.DamageSystem;

namespace BoxDefence.Towers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ElectroTower : ImprovingTower<ElectroTower.ElectroTowerLevel>, ILogicTowerAdapter, 
        IElectroTowerAdaper
    {
        #region Fields

        [Space]
        [SerializeField] private List<Enemy> _enemysInShootZone;
        [Header("Ñharacteristics")]
        [SerializeField] private float _colliderRadius = 3f;
        [SerializeField] private Damage _damage;
        [Header("Other")]
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private ElectroLine _electroLine;
        [SerializeField] private ElectroTowerLevel[] _electroTowerLevels;

        private CircleCollider2D _circleCollider2D;

        private Enemy _currentEnemy;

        private TowerCollisionHandling _towerCollisionHandling;

        #endregion

        #region Properties

        public IDamager Damage { get => _damage; }

        public LineRenderer Line { get => _lineRenderer; }

        public Enemy CurrentEnemy { get => _currentEnemy; set => _currentEnemy = value; }
        public List<Enemy> EnemysInShootZone { get => _enemysInShootZone; set => _enemysInShootZone = value; }

        public override ElectroTowerLevel[] Levels { get => _electroTowerLevels; }

        protected override TowerImrpover<ElectroTowerLevel> TowerImrpover { get; set; }

        #endregion

        #region ShootGunTowerLevel

        [Serializable]
        public class ElectroTowerLevel : ITowerImprovement
        {
            [field: SerializeField] public int Index { get; private set; }
            [field: Space]
            [field: SerializeField] public int PriceImprovement { get; private set; }
            [field: Space]
            [field: SerializeField] public float ColliderRadius { get; private set; }
            [field: SerializeField] public Damage Damage { get; private set; }
            [field: Space]
            [field: SerializeField] public Sprite TowerFoundation { get; private set; }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _circleCollider2D = GetComponent<CircleCollider2D>();

            _electroLine.Init(this);

            _towerCollisionHandling = new TowerCollisionHandling(this);
            TowerImrpover = new TowerImrpover<ElectroTowerLevel>(this);
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
            if (_currentEnemy != null)
                _electroLine.Shoot(_currentEnemy);
        }

        #endregion

        #region Public Methods

        public override void SetLevelCharacteristics(ElectroTowerLevel levelCharacteristic)
        {
            _damage = levelCharacteristic.Damage;
            _colliderRadius = levelCharacteristic.ColliderRadius;
            _circleCollider2D.radius = _colliderRadius;
            GetComponent<SpriteRenderer>().sprite = levelCharacteristic.TowerFoundation;
        }

        #endregion
    }
}
