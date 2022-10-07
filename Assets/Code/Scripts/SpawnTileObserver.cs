using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BoxDefence.Tilemaps;

namespace BoxDefence
{
    public class SpawnTileObserver : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<SpawnTile> _spawnTiles;
        [Space]
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private TilemapCollider2D _tilemapCollider2D;

        private int _maxSpawnTileCount = 0;
        private int _spawnTileCount = 0;

        #endregion

        #region Actions

        public event Action OnCreateAllEnemyBase;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _maxSpawnTileCount = GetSpawnTileCount();
        }
        private void OnEnable()
        {
            foreach (SpawnTile spawnTile in _spawnTiles)
                spawnTile.OnCreateEnemyBase += AddCount;
        }
        private void OnDisable()
        {
            foreach (SpawnTile spawnTile in _spawnTiles)
                spawnTile.OnCreateEnemyBase -= AddCount;
        }

        #endregion

        #region Private Methods

        private void AddCount()
        {
            _spawnTileCount++;
            if (CheckEnemyBaseCount() == true)
                OnCreateAllEnemyBase?.Invoke();
        }
        private bool CheckEnemyBaseCount()
        {
            if (_spawnTileCount == _maxSpawnTileCount)
                return true;
            else
                return false;
        }
        private int GetSpawnTileCount()
        {
            int minX = (int)_tilemapCollider2D.bounds.min.x;
            int minY = (int)_tilemapCollider2D.bounds.min.y;
            int maxX = (int)_tilemapCollider2D.bounds.max.x;
            int maxY = (int)_tilemapCollider2D.bounds.max.y;

            int spawnTilesCount = 0;

            for (int i = minX; i < maxX; i++)
            {
                for (int j = minY; j < maxY; j++)
                {
                    Vector3Int intPosition = new Vector3Int(i, j, 0);

                    SpawnTile tile = _tilemap.GetTile<SpawnTile>(intPosition);
                    if (tile != null)
                        spawnTilesCount++;
                }
            }

            return spawnTilesCount;
        }

        #endregion
    }
}
