using UnityEngine;
using BoxDefence.Towers;

namespace BoxDefence
{
    public class Cell : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Tower _tower;

        #endregion

        #region Public Methond

        public bool CanSetTower()
        {
            if (_tower == null)
                return true;
            else
                return false;
        }

        public void SetTower(Tower tower)
        {
            if (CanSetTower() == false)
                return;

            Tower createTower = Instantiate(tower, transform, true);

            _tower = createTower;
            _tower.SetTower(transform.position + Vector3.back * 2);
        }

        #endregion
    }
}
