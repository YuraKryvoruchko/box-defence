using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BoxDefence.Tilemaps
{
    [CreateAssetMenu(fileName = "SpawnTile", menuName = "Tile/SpawnTile", order = 5)]
    public class SpawnTile : NodeTile
    {
        #region Actions

        public event Action<Spawner> OnCreateSpanwer;

        #endregion

        #region Unity Methods

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            Debug.Log("SpawnTile time: " + Time.realtimeSinceStartup);
            SetTilemapForSpawner(tilemap, go);

            return base.StartUp(position, tilemap, go);
        }

        #endregion

        #region Private Methods

        private void SetTilemapForSpawner(ITilemap tilemap, GameObject go)
        {
            try
            {
                Tilemap currentTilemap = GetTilemap(tilemap);

                if (go.TryGetComponent(out Spawner spawner))
                    spawner.SetTilemap(currentTilemap);
                else
                    throw new Exception("GameObject dont have Spawner.cs");

                OnCreateSpanwer?.Invoke(spawner);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }
        private Tilemap GetTilemap(ITilemap tilemap)
        {
            Tilemap currentTilemap = tilemap.GetComponent<Tilemap>();
            if (currentTilemap == null)
                throw new Exception("tilemap dont have Tilemap Component");

            return currentTilemap;
        }

        #endregion
    }
}
