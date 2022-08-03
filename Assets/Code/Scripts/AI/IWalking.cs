using System.Collections.Generic;
using UnityEngine;

namespace BoxDefence.AI
{
    public interface IWalking
    {
        void ChangeWayPoints(List<Transform> wayPoints);
    }
}
