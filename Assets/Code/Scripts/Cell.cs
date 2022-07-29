using System;
using UnityEngine;
using BoxDefence.Towers;

namespace BoxDefence
{
    public class Cell : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Tower _tower;

        #endregion

        #region Properties

        public Tower Tower { get => _tower; }

        #endregion

        #region Public Methond

        public bool IsTowerSet()
        {
            if (_tower != null)
            {
                Debug.LogWarning("Cell is empty");

                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetTower(Tower tower)
        {
            try
            {
                if (IsTowerSet() == true)
                    throw new Exception("You are trying to install a tower, but the tower is installed!");

                Tower createTower = Instantiate(tower, transform, true);

                _tower = createTower;
                _tower.SetTower(transform.position + Vector3.back * 2);
            }
            catch(Exception exception)
            {
                Debug.LogException(exception);
            }
        }
        public void DeleteTower()
        {
            _tower.DeleteTower();
        }

        #endregion
    }
}
