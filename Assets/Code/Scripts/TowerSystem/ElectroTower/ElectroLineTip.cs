using UnityEngine;
using BoxDefence.DamageSystem;

namespace BoxDefence.Towers
{
    public class ElectroLineTip : MonoBehaviour, IDamager
    {
        #region Fields

        [SerializeField] private ElectroLine _electroLine;

        #endregion

        #region Public Methods

        public void DepleteBy(float percentageInDozens)
        {
            _electroLine.DepleteBy(percentageInDozens);
        }
        public void IncreaseBy(float percentageInDozens)
        {
            _electroLine.IncreaseBy(percentageInDozens);
        }
        public float GetDamage()
        {
            return _electroLine.GetDamage();
        }
        public DamageType GetDamageType()
        {
            return _electroLine.GetDamageType();
        }

        #endregion
    }
}
