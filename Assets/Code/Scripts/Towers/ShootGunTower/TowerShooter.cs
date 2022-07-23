using System;
using System.Threading.Tasks;
using UnityEngine;
using BoxDefence.AI;
using BoxDefence.Pooling;

namespace BoxDefence.Towers
{
    public class TowerShooter
    {
        private float _damage;
        private float _shootRate;

        private Bullet _bulletPrefab;

        private Transform _bulletSpawnPoint;

        private bool _canShoot = true;

        private ObjectPooler _objectPooler;

        public TowerShooter(IShooterTowerAdapter shooterTowerAdaper)
        {
            _damage = shooterTowerAdaper.Damage;
            _shootRate = shooterTowerAdaper.ShootRate;
            _bulletPrefab = shooterTowerAdaper.BulletPrefab;
            _bulletSpawnPoint = shooterTowerAdaper.SpawnPoint;

            _objectPooler = ObjectPooler.Instance;
        }

        public bool CanShoot()
        {
            return _canShoot;
        }

        public async void Shoot(Enemy enemy)
        {
            if (_canShoot == true)
            {
                Bullet bullet = _objectPooler.GetObject(_bulletPrefab,
                                                              _bulletSpawnPoint.position, 
                                                              Quaternion.identity);

                bullet.OnStart(_damage, enemy);

                _canShoot = false;

                await TimerBetweenShoots();
            }
        }
        private async Task TimerBetweenShoots
            ()
        {
            TimeSpan shootRateInMilliseconds = TimeSpan.FromSeconds(_shootRate);

            await Task.Delay((int)shootRateInMilliseconds.TotalMilliseconds);

            _canShoot = true;
        }
    }
}
