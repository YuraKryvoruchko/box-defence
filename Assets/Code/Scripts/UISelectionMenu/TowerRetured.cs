using System;
using UnityEngine;
using BoxDefence.Towers;
using AYellowpaper;

namespace BoxDefence.UI
{
    public interface ITowerReturing
    {
        IBaseTower GetBaseTowerPrefab();
    }
    public class TowerRetured : MonoBehaviour, ITowerReturing
    {
        [SerializeField] private InterfaceReference<IBaseTower, MonoBehaviour> _baseTowerPrefab;

        public IBaseTower GetBaseTowerPrefab()
        {
            if (_baseTowerPrefab == null)
                throw new Exception("_baseTowerPrefab is null!");

            return _baseTowerPrefab.Value;
        }
    }
}
