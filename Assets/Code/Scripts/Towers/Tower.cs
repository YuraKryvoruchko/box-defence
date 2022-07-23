using UnityEngine;

namespace BoxDefence.Towers
{
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField] private int _price = 10;
        [SerializeField] private int _priceReturn = 5;

        public int SetTower(Vector3 position)
        {
            transform.position = position;

            return _price;
        }
        public int DeleteTower()
        {
            Destroy(gameObject);

            return _priceReturn;
        }
    }
}
