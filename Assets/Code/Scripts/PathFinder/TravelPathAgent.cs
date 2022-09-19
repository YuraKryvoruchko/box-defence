using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Random = UnityEngine.Random;

namespace BoxDefence.PathFinderAI
{
    public class TravelPathAgent
    {
        #region Fields

        private Vector2 _spawnPoint;

        private PathFinder _pathFinder;

        #endregion

        #region Constructor

        public TravelPathAgent(Tilemap tilemap, Vector2 spawnPoint)
        {
            _pathFinder = new PathFinder(tilemap);
            _spawnPoint = spawnPoint;
        }

        #endregion

        #region Public Methods

        public List<Vector2> GetNewPath()
        {
            Transform target = GetTargetPoint();

            List<Vector2> newPath = _pathFinder.GetPath(_spawnPoint, target.position);

            return newPath;
        }
        public void SetSpawnPosition(Vector2 spawnPoint)
        {
            if (spawnPoint == null)
                throw new Exception("spawnPoint is null!");

            _spawnPoint = spawnPoint;
        }

        #endregion

        #region Private Methods

        private Transform GetTargetPoint()
        {
            WayTarget[] wayTargets = MonoBehaviour.FindObjectsOfType<WayTarget>();

            List<Transform> notFreeTargets = new List<Transform>();
            List<Transform> freeTargets = new List<Transform>();
            foreach (WayTarget wayTarget in wayTargets)
            {
                if (wayTarget.IsFree == true)
                    freeTargets.Add(wayTarget.GetTransform());
                else
                    notFreeTargets.Add(wayTarget.GetTransform());
            }

            if (freeTargets.Count > 0)
                return GetRandomTargetTransform(freeTargets);

            return GetRandomTargetTransform(notFreeTargets);
        }
        private Transform GetRandomTargetTransform(List<Transform> targets)
        {
            try
            {
                if (targets.Count == 0)
                    throw new Exception("List count is zero!");

                int index = Random.Range(0, targets.Count - 1);

                Transform target = targets[index];

                return target;
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);

                return default;
            }
        }

        #endregion
    }
}
