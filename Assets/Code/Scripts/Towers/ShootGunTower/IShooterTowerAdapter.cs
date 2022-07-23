using UnityEngine;

namespace BoxDefence.Towers
{
    public interface IShooterTowerAdapter
    {
        float Damage { get; }
        float ShootRate { get; }

        Bullet BulletPrefab { get; }

        Transform SpawnPoint { get; }
    }
}
