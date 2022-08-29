using BoxDefence.Towers;
using BoxDefence.WalletSystem;

namespace BoxDefence
{
    public class TowerCreator
    {
        #region Fields

        private TowerBuyer _towerBuyer;

        #endregion

        #region Constructor

        public TowerCreator(TowerBuyer towerBuyer)
        {
            _towerBuyer = towerBuyer;
        }

        #endregion

        #region Public Methods

        public void CreateTower(Cell cell, IBaseTower tower)
        {
            if (cell.IsTowerSet() == true)
                return;
            if (_towerBuyer.CanBuy(tower.Price) == false)
                return;

            cell.SetBaseTower(tower);

            _towerBuyer.BuyTower(cell.BaseTower);
        }

        #endregion
    }
}
