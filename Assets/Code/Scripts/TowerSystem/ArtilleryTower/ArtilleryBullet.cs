using System;
using UnityEngine;
using BoxDefence.Pooling;
using BoxDefence.DamageSystem;

namespace BoxDefence.Towers
{
    public class ArtilleryBullet : MonoBehaviour, IPoolObject, IDamager
    {
        #region Fields
        
        #endregion 

        #region Properties

        public PoolType PoolTypeObject => throw new NotImplementedException();

        #endregion

        #region Public Methods

        public void Init(Vector3 position, Quaternion rotation)
        {
            throw new NotImplementedException();
        }

        public void DepleteBy(float percentageInDozens)
        {
            throw new NotImplementedException();
        }
        public float GetDamage()
        {
            throw new NotImplementedException();
        }
        public DamageType GetDamageType()
        {
            throw new NotImplementedException();
        }
        public void IncreaseBy(float percentageInDozens)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
