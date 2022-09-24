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

        #endregion

        #region Unity Methods

        private void Start()
        {
            _spawner.Init(transform.position);
            _spawner.CreateWaves();
        }

        #endregion

        #region Public Methods

        public void SetTilemap(Tilemap tilemap)
        {
            _tilemap = tilemap;
            _spawner.SetTilemap(_tilemap);
        }

        public IPassedEnemyCounting GetPassedEnemyCounting()
        {
            throw new NotImplementedException();
        }
        public IDeadEnemyCounting GetDeadEnemyCounting()
        {
            throw new NotImplementedException();
        }
        public IEnemyWavesCounting GetEnemyWavesCounting()
        {
            return _spawner.GetEnemyWavesCounting();
        }

        #endregion
    }
}
