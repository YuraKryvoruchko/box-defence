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

        private void Awake()
        {
            base.Awake();

            _towerBuyer = new TowerBuyer(_wallet);
        }

        #endregion

        #region Public Methods

        public void ImproveTower()
        {
            try
            {
                if (CurrentCell.IsTowerSet() == false)
                    throw new Exception("The tower is not installed!");

                ITowerImprover currentTower = CurrentCell.BaseTower;

                int levelPrice = currentTower.GetNextLevelPrice();

                if (_towerBuyer.CanBuy(levelPrice) == false)
                    throw new Exception("Can not buy!");

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

                DisableMenu();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        #endregion

        #region Private Methods

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
