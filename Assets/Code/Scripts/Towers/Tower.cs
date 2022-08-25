using System;
using UnityEngine;

namespace BoxDefence.Towers
{
    public abstract class Tower : MonoBehaviour, ITowerPriceList
    {
        #region Fields

        [SerializeField] private int _price = 10;
        [SerializeField] private int _priceReturn = 5;

        #endregion

        #region Properties

        public int Price { get => _price; }
        public int PriceReturn { get => _priceReturn; }

        #endregion

        #region Actions

        public event Action<ITowerPriceList> OnSet;
        public event Action<ITowerPriceList> OnDelete;

        #endregion

        #region Public Methods

        public void SetTower(Vector3 position)
        {
            transform.position = position;

            OnSet?.Invoke(this);
        }
        public void Improve()
        {

        }
        public void DeleteTower()
        {
            Destroy(gameObject);

            OnDelete?.Invoke(this);
        }

        #endregion
    }
}
