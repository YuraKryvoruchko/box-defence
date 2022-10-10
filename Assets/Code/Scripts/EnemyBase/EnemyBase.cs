﻿using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using BoxDefence.Enumerating;

namespace BoxDefence
{
    public class EnemyBase : MonoBehaviour, IEnemyControlPointing
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

        #endregion

        #region Unity Methods

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
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        public void InitSpanwer()
        {
            _spawner.Init(transform.position);
        }

        public IPassedEnemyCounting GetPassedEnemyCounting()
        {
            return _spawner.GetPassedEnemyCounting();
        }
        public IDeadEnemyCounting GetDeadEnemyCounting()
        {
            return _spawner.GetDeadEnemyCounting();
        }
        public IEnemyWavesCounting GetEnemyWavesCounting()
        {
            return _spawner.GetEnemyWavesCounting();
        }

        #endregion
    }
}
