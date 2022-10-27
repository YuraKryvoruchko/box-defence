using System;
using UnityEngine;
using BoxDefence.DamageSystem;
using BoxDefence.AI;

namespace BoxDefence.Towers
{
    public class ArtilleryGun : MonoBehaviour, IDamager
    {
        #region Fields

        private IArtilleryTowerShooting _artilleryTowerShooting;

        #endregion

        #region Public Methods

        public void Init(IArtilleryTowerShooting artilleryTowerShooting)
        {
            _artilleryTowerShooting = artilleryTowerShooting;
        }

        public void Shoot(Enemy enemy)
        {
            throw new NotImplementedException();
        }
        public bool CanShoot()
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
        public void DepleteBy(float percentageInDozens)
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
