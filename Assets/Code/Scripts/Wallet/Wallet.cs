using System;
using UnityEngine;

namespace BoxDefence.Wallet
{
    [CreateAssetMenu(fileName = "WalletSO", menuName = "Box Defence/WalletSO", order = 0)]
    public class Wallet : ScriptableObject
    {
        #region Fields

        [SerializeField] private int _countMoney = 0;

        private const int MIN_COUNT_MONEY = 0;
        private const int MAX_COUNT_MONEY = 3000;

        #endregion

        #region Public Methods

        public int GetCoutnMoney()
        {
            return _countMoney;
        }
        public int GetAmountOfWalletFreeSpace()
        {
            int freeSpace = MAX_COUNT_MONEY - _countMoney;

            return freeSpace;
        }
        public int GetMoney(int count)
        {
            try
            {
                if(_countMoney - count >= MIN_COUNT_MONEY)
                {
                    _countMoney -= count;

                    return count;
                }
                else
                {
                    throw new Exception("You are trying to take more money from your wallet than you have");
                }
            }
            catch (Exception exception)
            {    
                Debug.LogException(exception);

                return MIN_COUNT_MONEY;
            }
        }
        public void AddMoney(int count)
        {
            try
            {
                if (_countMoney + count <= MAX_COUNT_MONEY)
                    _countMoney += count;
                else
                    throw new Exception("Exceeding the maximum value of the amount of money in the wallet");
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }
        public bool CanAddMoney(int count)
        {
            if (_countMoney + count <= MAX_COUNT_MONEY)
                return true;
            else
                return false;
        }
        public bool CanGetMoney(int count)
        {
            if (_countMoney - count >= MIN_COUNT_MONEY)
                return true;
            else
                return false;
        }

        #endregion
    }
}
