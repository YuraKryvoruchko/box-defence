using UnityEngine;
using BoxDefence.AI;
using BoxDefence.Pooling;

namespace BoxDefence.Towers
{
    public class TowerShooter : ICanShooting
    {
        #region Fields

        private IShooterTowerAdapter _shooterTowerAdaper;

        private ObjectPooler _objectPooler;

        private ShootingBlocator _shootingBlocator;

        #endregion

        #region Constructor

        public TowerShooter(IShooterTowerAdapter shooterTowerAdaper)
        {
            _shooterTowerAdaper = shooterTowerAdaper;

            _objectPooler = ObjectPooler.Instance;
            _shootingBlocator = new ShootingBlocator();
        }

        #endregion

        #region Fields

        public void Shoot(Enemy enemy)
        {
            if (CanShoot() == false)
                return;

            Bullet bullet = _objectPooler.GetObject(_shooterTowerAdaper.BulletPrefab,
                                                    _shooterTowerAdaper.SpawnPoint.position,
                                                    Quaternion.identity);

            bullet.OnStart(_shooterTowerAdaper.Damage, enemy);

            _shootingBlocator.BlockShootingOn(_shooterTowerAdaper.ShootRate);
        }
        public bool CanShoot()
        {
            return _shootingBlocator.CanShoot();
        }

        #endregion
    }
}
