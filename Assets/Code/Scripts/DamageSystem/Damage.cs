using System;
using UnityEngine;

namespace BoxDefence.Damage
{
    public interface IDamager
    {
        float GetDamage();
        DamageType GetDamageType();
        void DepleteBy(float percentageInDozens);
    }

    [Serializable]
    public class Damage : IDamager
    {
        #region Fields

        [SerializeField] private float _damage;
        [Space]
        [SerializeField] private DamageType _damageType;

        private const int MAX_PERCENTAGE = 100;
        private const int MIN_PERCENTAGE = 0;

        #endregion

        #region Constructor

        public Damage(float damage, DamageType damageType)
        {
            _damage = damage;
            _damageType = damageType;
        }

        #endregion

        #region Public Methods

        public float GetDamage()
        {
            return _damage;
        }
        public DamageType GetDamageType()
        {
            return _damageType;
        }
        public void DepleteBy(float percentageInDozens)
        {
            if (percentageInDozens < MIN_PERCENTAGE)
                throw new Exception("The percentage cannot be less than zero");

            float damageOnOnePercentage = _damage / MAX_PERCENTAGE;
            float lostDamage = damageOnOnePercentage * percentageInDozens;

            _damage -= lostDamage;
        }

        #endregion
    }
}
