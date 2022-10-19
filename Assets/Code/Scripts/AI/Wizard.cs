using BoxDefence.Damage;

namespace BoxDefence.AI
{
    public class Wizard : Enemy
    {
        #region Fields

        private const DamageType BLOCKED_DAMAGE_TYPE = DamageType.Electrical;

        #endregion

        #region Public Methods

        public override void TakeDamage(IDamager damager)
        {
            float damage = damager.GetDamage();

            if (damager.GetDamageType() == BLOCKED_DAMAGE_TYPE)
                damage -= 20 * (damage / 100);

            base.TakeDamage(damage);
        }

        #endregion
    }
}
