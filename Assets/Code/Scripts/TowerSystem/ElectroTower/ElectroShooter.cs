using UnityEngine;
using BoxDefence.AI;
using BoxDefence.DamageSystem;

namespace BoxDefence.Towers
{
    public class ElectroShooter : IShooting
    {
        #region Fields

        private IElectroTowerAdaper _electroTowerAdaper;

        private IDamager _towerDamager;

        private LineRenderer _line;

        private const int INDEX_STARTING_POINT = 0;
        private const int INDEX_ENDING_POINT = 1;

        #endregion

        #region Constructor

        public ElectroShooter(IElectroTowerAdaper electroTowerAdaper)
        {
            _electroTowerAdaper = electroTowerAdaper;
            _towerDamager = electroTowerAdaper.Damage;
            _line = _electroTowerAdaper.Line;
        }

        #endregion

        #region Public Methods

        public void Shoot(Enemy enemy)
        {
            DrawLine(enemy.transform.position);

            float damage = _towerDamager.GetDamage() * Time.deltaTime;
            Damage damager = new Damage(damage, _towerDamager.GetDamageType());

            enemy.TakeDamage(damager);
        }

        #endregion

        #region Private Methods

        private void DrawLine(Vector3 endPoint)
        {
            _line.SetPosition(INDEX_STARTING_POINT, _line.transform.position);

            Vector3 endPosition = new Vector3(endPoint.x,
                                              endPoint.y,
                                              _line.transform.position.z);

            _line.SetPosition(INDEX_ENDING_POINT, endPosition);
        }

        #endregion
    }
}
