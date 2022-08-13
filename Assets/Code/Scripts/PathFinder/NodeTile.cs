using UnityEngine;
using UnityEngine.Tilemaps;

namespace BoxDefence.Tilemaps
{
    [CreateAssetMenu(fileName = "NodeTile", menuName = "Tile/NodeTile", order = 4)]
    public class NodeTile : Tile
    {
        #region Fields

        [field: SerializeField] public int Patency { get; private set; }
        [field: SerializeField] public bool Walkable { get; private set; }

        #endregion
    }
}
