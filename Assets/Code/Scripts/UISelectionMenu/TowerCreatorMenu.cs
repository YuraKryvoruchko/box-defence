using UnityEngine;
using BoxDefence.Towers;
using BoxDefence.WalletSystem;

namespace BoxDefence.UI
{
    public class TowerCreatorMenu : SelectionMenu
    {
        #region Fields

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

        public void CreateTower(Tower towerPrefab)
        {
            if (CurrentCell.IsTowerSet() == true)
                return;

            if (_towerBuyer.CanBuyTower(towerPrefab.Price) == false)
                return;

            CurrentCell.SetTower(towerPrefab);

            _towerBuyer.BuyTower(CurrentCell.Tower);

            DisableMenu();
        }

        #endregion
    }
}
