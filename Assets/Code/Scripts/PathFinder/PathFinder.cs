using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BoxDefence.PathFinderAI
{
    public class PathFinder
    {
        #region Fields

        private Tilemap _tilemap;

        private List<Vector2> _pathToTarget;

        private List<Node> _waitingNodes = new List<Node>();
        private List<Node> _freeNodes = new List<Node>();
        private List<Node> _checkedNodes = new List<Node>();

        #endregion

        #region Constructor

        public PathFinder(Tilemap tilemap)
        {
            _tilemap = tilemap;
        }

        #endregion

        #region Public Methods

        public List<Vector2> GetPath(Vector2 startPosition, Vector2 targetPosition)
        {
            InitializedLists();

            startPosition = new Vector2(Mathf.Round(startPosition.x), Mathf.Round(startPosition.y));
            targetPosition = new Vector2(Mathf.Round(targetPosition.x), Mathf.Round(targetPosition.y));

            if(startPosition == targetPosition)
                return _pathToTarget;

            Node startNode = new Node(0, startPosition, targetPosition, null, _tilemap);
            _checkedNodes.Add(startNode);
            _waitingNodes.AddRange(startNode.GetNeighbourNodes());

            while (_waitingNodes.Count > 0)
            {
                Node nodeToCheck = _waitingNodes.Where(x => x.F == _waitingNodes.Min(y => y.F)).FirstOrDefault();

                if (nodeToCheck.Position == targetPosition)
                {
                    _pathToTarget = CalculatePathFromNode(nodeToCheck);
                    _pathToTarget.Reverse();
                    return _pathToTarget;
                }

                CheckNodeForWalkable(nodeToCheck);
            }
            _freeNodes = _checkedNodes;

            _pathToTarget.Reverse();
            return _pathToTarget;
        }
        public void OnDrawPath()
        {
            foreach (var item in _checkedNodes)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(new Vector2(item.Position.x, item.Position.y), 0.1f);
            }
            if (_pathToTarget != null)
                foreach (var item in _pathToTarget)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(new Vector2(item.x, item.y), 0.2f);
                }
        }

        #endregion

        #region Private Methods

        private void InitializedLists()
        {
            _pathToTarget = new List<Vector2>();

            _waitingNodes = new List<Node>();
            _freeNodes = new List<Node>();
            _checkedNodes = new List<Node>();
        }
        private List<Vector2> CalculatePathFromNode(Node node)
        {
            var path = new List<Vector2>();
            Node currentNode = node;

            while (currentNode.PreviosNode != null)
            {
                path.Add(new Vector2(currentNode.Position.x, currentNode.Position.y));
                currentNode = currentNode.PreviosNode;
            }

            return path;
        }
        private void CheckNodeForWalkable(Node nodeToCheck)
        {
            if (nodeToCheck.Walkable == false)
            {
                _waitingNodes.Remove(nodeToCheck);
                _checkedNodes.Add(nodeToCheck);
            }
            else if (nodeToCheck.Walkable == true)
            {
                _waitingNodes.Remove(nodeToCheck);
                if (_checkedNodes.Where(x => x.Position == nodeToCheck.Position).Any() == false)
                {
                    _checkedNodes.Add(nodeToCheck);
                    _waitingNodes.AddRange(nodeToCheck.GetNeighbourNodes());
                }
            }
        }

        #endregion
    }
}