using UnityEngine;

namespace BoxDefence.Pooling
{
    public interface IPool
    {
        PoolType PoolTypeObject { get; }

        void Init(Vector3 position, Quaternion rotation);
    }
}
