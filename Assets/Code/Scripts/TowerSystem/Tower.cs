using System;
using UnityEngine;
using BoxDefence.Pooling;

namespace BoxDefence.Towers
{
    public abstract class Tower : MonoBehaviour, ITowerPricelist, IPoolObject 
    {
        #region Fields

        [SerializeField] private PoolType _poolType;
        [Space]
        [SerializeField] private int _price = 10;
        [SerializeField] private int _priceReturn = 5;

        #endregion

        #region Properties

        public PoolType PoolTypeObject { get => _poolType; }

        public int Price { get => _price; }
        public int PriceReturn { get => _priceReturn; protected set => _priceReturn = value; }

        #endregion

        #region Actions

        public event Action<ITowerPricelist> OnSet;
        public event Action<ITowerPricelist> OnDelete;
        public event Action<ITowerImprovement> OnImproving;

        #endregion

        #region Public Methods

        public void Init(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
            gameObject.SetActive(true);
        }
        public void SetTower(Vector3 position)
        {
            transform.position = position;

            OnSet?.Invoke(this);
        }
        public void DeleteTower()
        {
            Destroy(gameObject);

            OnDelete?.Invoke(this);
        }

        #endregion
    }
}
