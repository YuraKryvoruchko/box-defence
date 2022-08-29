using BoxDefence.AI;

namespace BoxDefence.Towers
{
    public interface IShooting
    {
        void Shoot(Enemy enemy);
    }
    public interface ICanShooting : IShooting
    {
        bool CanShoot();
    }
}
