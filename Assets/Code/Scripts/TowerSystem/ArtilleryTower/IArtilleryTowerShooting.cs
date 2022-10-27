using BoxDefence.DamageSystem;

namespace BoxDefence.Towers
{
    public interface IArtilleryTowerShooting
    {
        public float ShootRate { get; }
        public ArtilleryBullet ArtilleryBullet { get; }
        public Damage Damage { get; }
    }
}
