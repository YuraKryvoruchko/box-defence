using UnityEngine;
using BoxDefence.WalletSystem;

namespace BoxDefence
{
    public class TowerInstaller : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _mainCamera;

        [SerializeField] private Wallet _wallet;

        private TowerPicker _towerPicker;
        private CellFinder _cellFinder;
        private TowerBuyer _towerBuyer;

        private InputSystem _inputSystem;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _inputSystem = new InputSystem();
        }
        private void OnEnable()
        {
            _inputSystem.Enable();
        }
        private void OnDisable()
        {
            _inputSystem.Disable();
        }
        private void Start()
        {
            _towerPicker = new TowerPicker();
            _towerBuyer = new TowerBuyer(_wallet);
            _cellFinder = new CellFinder(_inputSystem, _mainCamera);
            _cellFinder.ClickingOnCell += SetTower;
        }

        #endregion

        #region Private Methods

        private void SetTower(Cell cell)
        {
            if (_towerPicker.IsTowerSelected() == true)
            {
                if (cell.IsTowerSet() == false)
                {
                    if (_towerBuyer.CanBuyTower(_towerPicker.Tower.Price))
                    {
                        cell.SetTower(_towerPicker.Tower);

                        _towerBuyer.BuyTower(cell.Tower);
                    }
                }
            }
        }

        #endregion
    }
}
