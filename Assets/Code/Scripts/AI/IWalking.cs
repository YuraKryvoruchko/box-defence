using System.Collections.Generic;
using UnityEngine;

namespace BoxDefence.AI
{
    public interface IWalking
    {
        void ChangePath(List<Vector2> path);
    }
}
