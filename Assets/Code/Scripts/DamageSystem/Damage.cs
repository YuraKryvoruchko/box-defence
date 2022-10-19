using System;
using UnityEngine;

namespace BoxDefence.Damage
{
    public interface IDamager
    {
        float GetDamage();
        DamageType GetDamageType();
    }

    [Serializable]
    public class Damage : IDamager
    {
        #region Fields

        [SerializeField] private float _damage;
        [Space]
        [SerializeField] private DamageType _damageType;

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

        #endregion
    }
}
