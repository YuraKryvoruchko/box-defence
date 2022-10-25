using UnityEngine;
using BoxDefence.AI;
using BoxDefence.DamageSystem;

namespace BoxDefence.Towers
{
    public class ElectroLine : MonoBehaviour, IDamager, IShooting
    {
        #region Fields

        [SerializeField] private ElectroLineTip _electroLineTip;

        private ElectroShooter _electroShooter;

        private const bool ENABLED_ELECTRO_LINE_TIP = true;
        private const bool DISABLED_ELECTRO_LINE_TIP = false;

        #endregion

        #region Public Methods

        public void Init(IElectroTowerAdaper electroTowerAdaper)
        {
            _electroShooter = new ElectroShooter(electroTowerAdaper);
        }

        public void Shoot(Enemy enemy)
        {
            _electroLineTip.enabled = ENABLED_ELECTRO_LINE_TIP;

            MoveTipTo(enemy.transform.position);
            _electroShooter.Shoot(enemy);

            _electroLineTip.enabled = DISABLED_ELECTRO_LINE_TIP;
        }

        public float GetDamage()
        {
            return _electroShooter.GetDamage();
        }
        public DamageType GetDamageType()
        {
            return _electroShooter.GetDamageType();
        }
        public void DepleteBy(float percentageInDozens)
        {
            _electroShooter.DepleteBy(percentageInDozens);
        }
        public void IncreaseBy(float percentageInDozens)
        {
            _electroShooter.IncreaseBy(percentageInDozens);
        }

        #endregion

        #region Private Methods

        private void MoveTipTo(Vector3 pointPosition)
        {
            _electroLineTip.transform.position = pointPosition;
        }

        #endregion
    }
}
