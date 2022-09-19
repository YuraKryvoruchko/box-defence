using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BoxDefence
{
    public class EnemyControlPoint : MonoBehaviour, IEnemyControlPointing
    {
        #region Fields

        [SerializeField] private Tilemap _tilemap;
        [Space]
        [SerializeField] private Spawner _spawner;

        #endregion

        #region Actions

        public event Action OnAddPassedEnemy;
        public event Action OnRemovePassedEnemy;
        public event Action OnAddDeadEnemy;
        public event Action OnRemoveDeadEnemy;
        public event Action OnAddEnemyWaves;
        public event Action OnRemoveEnemyWaves;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _spawner.Init(transform.position);
        }
        private void OnEnable()
        {
            _spawner.OnAddDeadEnemy += () => OnAddDeadEnemy?.Invoke();
            _spawner.OnAddEnemyWaves += () => OnAddEnemyWaves?.Invoke();
            _spawner.OnAddPassedEnemy += () => OnAddPassedEnemy?.Invoke();
            _spawner.OnRemoveDeadEnemy += () => OnRemoveDeadEnemy?.Invoke();
            _spawner.OnRemoveEnemyWaves += () => OnRemoveEnemyWaves?.Invoke();
            _spawner.OnRemovePassedEnemy += () => OnRemovePassedEnemy?.Invoke();
        }
        private void OnDisable()
        {
            _spawner.OnAddDeadEnemy -= () => OnAddDeadEnemy?.Invoke();
            _spawner.OnAddEnemyWaves -= () => OnAddEnemyWaves?.Invoke();
            _spawner.OnAddPassedEnemy -= () => OnAddPassedEnemy?.Invoke();
            _spawner.OnRemoveDeadEnemy -= () => OnRemoveDeadEnemy?.Invoke();
            _spawner.OnRemoveEnemyWaves -= () => OnRemoveEnemyWaves?.Invoke();
            _spawner.OnRemovePassedEnemy -= () => OnRemovePassedEnemy?.Invoke();
        }
        private void Start()
        {
            _spawner.CreateWaves();
        }

        #endregion

        #region Public Methods

        public void SetTilemap(Tilemap tilemap)
        {
            _tilemap = tilemap;
            _spawner.SetTilemap(_tilemap);
        }

        public int GetDeadEnemyCount()
        {
            return _spawner.GetDeadEnemyCount();
        }
        public int GetEnemyWavesCount()
        {
            return _spawner.GetEnemyWavesCount();
        }
        public int GetMaxDeadEnemyCount()
        {
            return _spawner.GetMaxDeadEnemyCount();
        }
        public int GetMaxEnemyWavesCount()
        {
            return _spawner.GetMaxEnemyWavesCount();
        }
        public int GetMaxPassedEnemyCount()
        {
            return _spawner.GetMaxPassedEnemyCount();
        }
        public int GetPassedEnemyCount()
        {
            return _spawner.GetPassedEnemyCount();
        }

        #endregion
    }
}
