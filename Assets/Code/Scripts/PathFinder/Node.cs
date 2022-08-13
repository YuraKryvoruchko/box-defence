using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BoxDefence.Tilemaps;

namespace BoxDefence.PathFinderAI
{
    public class Node
    {
        #region Fields

        private Vector2 _position;
        private Vector2 _targetPosition;
        private Node _previosNode;

        private Tilemap _tilemap;

        private bool _walkable;

        private int _f;
        private int _g;
        private int _h;
        private int _patency = 0;

        private const int DIFFERENCE_IN_TARGET_DISTANCE = 1;

        #endregion

        #region Fields

        public Vector2 Position { get => _position; }
        public Vector2 TargetPosition { get => _targetPosition; }

        public Node PreviosNode { get => _previosNode; }

        public bool Walkable { get => _walkable; }

        public int F { get => _f; }
        public int G { get => _g; }
        public int H { get => _h; }

        #endregion

        #region Constructor

        public Node(int g, Vector2 nodePosition, Vector2 targetPosition, Node previosNode, Tilemap tilemap)
        {
            _position = nodePosition;
            _targetPosition = targetPosition;
            _previosNode = previosNode;
            _tilemap = tilemap;

            SetNodeDynamicCharacteristic();
            _g = g + _patency;
            _h = (int)Mathf.Abs(targetPosition.x - _position.x) + (int)Mathf.Abs(targetPosition.y - _position.y);
            _f = _g + _h;
        }

        #endregion

        #region Public Methods

        public List<Node> GetNeighbourNodes()
        {
            List<Node> neighbourNodes = new List<Node>();

            int newG = _g + DIFFERENCE_IN_TARGET_DISTANCE;

            neighbourNodes.Add(new Node(newG, _position + Vector2.left, _targetPosition, this, _tilemap));
            neighbourNodes.Add(new Node(newG, _position + Vector2.right, _targetPosition, this, _tilemap));
            neighbourNodes.Add(new Node(newG, _position + Vector2.down, _targetPosition, this, _tilemap));
            neighbourNodes.Add(new Node(newG, _position + Vector2.up, _targetPosition, this, _tilemap));

            return neighbourNodes;
        }

        #endregion

        #region Private Methods

        private void SetNodeDynamicCharacteristic()
        {
            Vector3Int position = new Vector3Int(Vector2Int.FloorToInt(Position).x,
                                                 Vector2Int.FloorToInt(Position).y,
                                                 Vector3Int.FloorToInt(_tilemap.transform.position).z);

            NodeTile nodeTile = _tilemap.GetTile<NodeTile>(position);
            if(nodeTile != null)
            {
                _patency = nodeTile.Patency;
                _walkable = nodeTile.Walkable;
            }
        }

        #endregion
    }
}
