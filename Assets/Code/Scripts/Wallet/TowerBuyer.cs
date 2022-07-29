﻿using System;
using UnityEngine;
using BoxDefence.Towers;

namespace BoxDefence.WalletSystem
{
    public class TowerBuyer
    {
        #region Fields

        private Wallet _wallet;

        #endregion

        #region Constructor

        public TowerBuyer(Wallet wallet)
        {
            _wallet = wallet;
        }

        #endregion

        #region Public Methods

        public bool CanBuyTower(int price)
        {
            bool canBuy = _wallet.CanGetMoney(price);

            if (canBuy == true)
                return true;
            else
                Debug.LogWarning("You don`t buy tower");

            return false;
        }

        public void BuyTower(ITowerPriceList towerPriceList)
        {
            if (_wallet.CanGetMoney(towerPriceList.Price) == true)
            {
                _wallet.GetMoney(towerPriceList.Price);

                towerPriceList.OnDelete += ReturnMoney;
                towerPriceList.OnSet -= BuyTower;
            }
            else
            {
                throw new Exception();
            }
        }

        #endregion

        #region Private Methods

        private void ReturnMoney(ITowerPriceList towerPriceList)
        {
            if (_wallet.CanAddMoney(towerPriceList.PriceReturn) == true)
            {
                _wallet.AddMoney(towerPriceList.PriceReturn);

                towerPriceList.OnDelete -= ReturnMoney;
            }
            else
            {
                Debug.LogWarning("I dont add money");
            }
        }

        #endregion
    }
}

