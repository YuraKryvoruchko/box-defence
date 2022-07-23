using UnityEngine;

namespace BoxDefence.Pooling
{
    public interface IPool
    {
        void Init(Vector3 position, Quaternion rotation);
    }
}
