using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using BoxDefence.Enumerating;

namespace BoxDefence.Tilemaps
{
    [CreateAssetMenu(fileName = "SpawnTile", menuName = "Tile/SpawnTile", order = 5)]
    public class SpawnTile : NodeTile
    {
        #region Actions

        public event Action OnCreateEnemyBase;
        public event Action<IEnemyControlPointing> OnBroadcastEnemyBase;

        #endregion

        #region Unity Methods

        public override bool StartUp(Vector3Int position, ITilemap iTilemap, GameObject go)
        {
            try
            {
                Tilemap tilemap = GetTilemap(iTilemap);
                Vector3 basePosition = new Vector3(position.x, position.y, position.z); 

                EnemyBase enemyBase = go.GetComponent<EnemyBase>();
                InitEnemyBase(enemyBase, tilemap, basePosition);

                OnBroadcastEnemyBase?.Invoke(enemyBase);
                OnCreateEnemyBase?.Invoke();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }

            return base.StartUp(position, iTilemap, go);
        }

        #endregion

        #region Private Methods

        private void InitEnemyBase(EnemyBase enemyBase, Tilemap tilemap, Vector3 basePosition)
        {
            enemyBase.SetTilemap(tilemap);
            enemyBase.SetPosition(basePosition);
            enemyBase.InitSpanwer();
        }
        private Tilemap GetTilemap(ITilemap tilemap)
        {
            try
            {
                Tilemap currentTilemap = tilemap.GetComponent<Tilemap>();
                if (currentTilemap == null)
                    throw new Exception("tilemap dont have Tilemap Component");

                return currentTilemap;
            }
            catch(Exception exception)
            {
                Debug.LogException(exception);

                return default;
            }
        }

        #endregion
    }
}
