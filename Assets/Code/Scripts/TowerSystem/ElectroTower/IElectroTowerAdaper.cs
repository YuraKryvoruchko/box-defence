using UnityEngine;

namespace BoxDefence.Towers
{
    public interface IElectroTowerAdaper
    {
        float Damage { get; }

        LineRenderer Line { get; }
    }
}
