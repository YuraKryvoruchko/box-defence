using UnityEngine;
using BoxDefence.Pooling;

namespace BoxDefence.Towers
{
    public interface IBaseTower : ITowerImprover, ITowerPricelist, IPoolObject
    {
        void SetTower(Vector3 position);
        void DeleteTower();
    }
}
