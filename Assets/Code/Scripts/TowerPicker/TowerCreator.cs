using BoxDefence.Towers;

namespace BoxDefence
{
    public class TowerCreator
    {
        private CellFinder _cellFinder;
        private Cell _cell;

        public TowerCreator(CellFinder cellFinder)
        {
            _cellFinder = cellFinder;
            _cellFinder.ClickingOnCell += SetCell;
            ReturedTower.OnPickTower += CreateTower;
        }

        private void SetCell(Cell cell)
        {
            _cell = cell;
        }
        private void CreateTower(Tower tower)
        {
            if (_cell.IsTowerSet() == false)
                _cell.SetTower(tower);
        }
    }
}
