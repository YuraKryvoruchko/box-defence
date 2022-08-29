using UnityEngine;

namespace BoxDefence.Towers
{
    public abstract class ImprovingTower<TowerLevelType> : Tower, IBaseTower,
        ITowerCharacteristic<TowerLevelType> where TowerLevelType : ITowerImprovement
    {
        #region Fields

        private const int IMPROVEMENT_PRICE_DIVIDER = 2;

        #endregion

        #region Properties

        protected abstract TowerImrpover<TowerLevelType> TowerImrpover { get; set; }

        public abstract TowerLevelType[] Levels { get; }

        #endregion

        #region Public Methods

        public void Improve()
        {
            TowerImrpover.ImproveToNextLevel();

            ITowerImprovement level = TowerImrpover.GetCurrentLevel();

            UpdatePriceReturn(level);
        }
        public int GetNextLevelPrice()
        {
            return TowerImrpover.GetNextLevel().PriceImprovement;
        }
        public bool IsMaxLevel()
        {
            return TowerImrpover.IsMaxLevel();
        }
        public ITowerImprovementPricelist GetTowerImprovementPricelist()
        {
            ITowerImprovementPricelist towerImprovementPricelist = TowerImrpover.GetCurrentLevel();

            return towerImprovementPricelist;
        }

        public abstract void SetLevelCharacteristics(TowerLevelType levelCharacteristic);

        #endregion

        #region Private Methods

        private void UpdatePriceReturn(ITowerImprovementPricelist towerImprovementPricelist)
        {
            PriceReturn += towerImprovementPricelist.PriceImprovement / IMPROVEMENT_PRICE_DIVIDER;
        }

        #endregion
    }
}
