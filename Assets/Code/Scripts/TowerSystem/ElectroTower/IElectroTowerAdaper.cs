using UnityEngine;
using BoxDefence.DamageSystem;

namespace BoxDefence.Towers
{
    public interface IElectroTowerAdaper
    {
        IDamager Damage { get; }

        LineRenderer Line { get; }
    }
}
