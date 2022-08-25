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

        private TowerCreator _towerCreator;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            base.Awake();

            _towerBuyer = new TowerBuyer(_wallet);
            _towerCreator = new TowerCreator(_towerBuyer);
        }

        #endregion

        #region Public Methods

        public void CreateTower(Tower towerPrefab)
        {
            _towerCreator.CreateTower(CurrentCell, towerPrefab);

            DisableMenu();
        }

        #endregion
    }
}
