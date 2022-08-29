using UnityEngine;
using BoxDefence.AI;
using BoxDefence.Pooling;

namespace BoxDefence.Towers
{
    public class TowerShooter : ICanShooting
    {
        #region Fields

        private float _damage;
        private float _shootRate;

        private Bullet _bulletPrefab;

        private Transform _bulletSpawnPoint;

        private ObjectPooler _objectPooler;

        private ShootingBlocator _shootingBlocator;

        #endregion

        #region Constructor

        public TowerShooter(IShooterTowerAdapter shooterTowerAdaper)
        {
            _damage = shooterTowerAdaper.Damage;
            _shootRate = shooterTowerAdaper.ShootRate;
            _bulletPrefab = shooterTowerAdaper.BulletPrefab;
            _bulletSpawnPoint = shooterTowerAdaper.SpawnPoint;

            _objectPooler = ObjectPooler.Instance;
            _shootingBlocator = new ShootingBlocator();
        }

        #endregion

        #region Fields

        public void Shoot(Enemy enemy)
        {
            if (CanShoot() == true)
            {
                Bullet bullet = _objectPooler.GetObject(_bulletPrefab,
                                                        _bulletSpawnPoint.position, 
                                                        Quaternion.identity);

                bullet.OnStart(_damage, enemy);

                _shootingBlocator.BlockShootingOn(_shootRate);
            }
        }
        public bool CanShoot()
        {
            return _shootingBlocator.CanShoot();
        }

        #endregion
    }
}
