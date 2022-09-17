using System;
using UnityEngine;
using UnityEngine.UI;
using BoxDefence.Towers;
using BoxDefence.WalletSystem;

namespace BoxDefence.UI
{
    public class InteractionMenu : SelectionMenu
    {
        #region Fields

        [Space]
        [SerializeField] private Sprite _improveSprite;
        [SerializeField] private Sprite _maxLevelSprite;
        [Space]
        [SerializeField] private Image _improveButtonImage;
        [Space]
        [SerializeField] private Wallet _wallet;

        private TowerBuyer _towerBuyer;

        #endregion

        #region Unity Methods

        protected void Awake()
        {
            base.Awake();

            _towerBuyer = new TowerBuyer(_wallet);
        }
        protected void OnEnable()
        {
            base.OnEnable();

            OnShowMenu += () => UpdateImproveButtonSprite(CurrentCell.BaseTower);
        }
        protected void OnDisable()
        {
            OnShowMenu -= () => UpdateImproveButtonSprite(CurrentCell.BaseTower);
        }

        #endregion

        #region Public Methods

        public void ImproveTower()
        {
            try
            {
                if (CurrentCell.IsTowerSet() == false)
                    throw new Exception("The tower is not installed!");
                if (CanBuyTower() == false)
                    return;

                ITowerImprover currentTower = CurrentCell.BaseTower;
                currentTower.Improve();

                _towerBuyer.BuyImprovement(currentTower.GetTowerImprovementPricelist());

                UpdateImproveButtonSprite(currentTower);
            }
            catch(Exception exception)
            {
                Debug.LogException(exception);
            }
        }
        public void DeleteTower()
        {
            try
            {
                if (CurrentCell.IsTowerSet() == false)
                    throw new Exception("The tower is not installed!");

                CurrentCell.DeleteTower();

                HideMenu();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        #endregion

        #region Private Methods

        private bool CanBuyTower()
        {
            ITowerImprover currentTower = CurrentCell.BaseTower;
            int levelPrice = currentTower.GetNextLevelPrice();

            if (_towerBuyer.CanBuy(levelPrice) == false)
                return false;

            return true;
        }
        private void UpdateImproveButtonSprite(ITowerImprover tower)
        {
            if (tower.IsMaxLevel() == true)
                _improveButtonImage.sprite = _maxLevelSprite;
            else
                _improveButtonImage.sprite = _improveSprite;
        }

        #endregion
    }
}
