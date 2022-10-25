using UnityEngine;
using BoxDefence.DamageSystem;

namespace BoxDefence.Towers
{
    public interface IShooterTowerAdapter
    {
        IDamager Damage { get; }
        float ShootRate { get; }

        Bullet BulletPrefab { get; }

        Transform SpawnPoint { get; }
    }
}
