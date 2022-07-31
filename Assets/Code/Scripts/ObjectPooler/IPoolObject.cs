using UnityEngine;

namespace BoxDefence.Pooling
{
    public interface IPoolObject
    {
        PoolType PoolTypeObject { get; }

        void Init(Vector3 position, Quaternion rotation);
    }
}
