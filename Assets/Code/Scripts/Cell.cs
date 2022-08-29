using System;
using UnityEngine;
using BoxDefence.Towers;
using BoxDefence.Pooling;
using AYellowpaper;

namespace BoxDefence
{
    public class Cell : MonoBehaviour
    {
        #region Fields

        [SerializeField] private InterfaceReference<IBaseTower, MonoBehaviour> _baseTower;

        private ObjectPooler _objectPooler;

        #endregion

        #region Properties

        public IBaseTower BaseTower { get => _baseTower.Value; 
                                      private set => _baseTower.Value = value; }

        #endregion

        #region Unity Methods

        private void Start()
        {
            _objectPooler = ObjectPooler.Instance;
        }

        #endregion

        #region Public Methond

        public bool IsTowerSet()
        {
            if (_baseTower.Value != null)
            {
                Debug.LogWarning("Cell is empty");

                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetBaseTower(IBaseTower tower)
        {
            try
            {
                if (IsTowerSet() == true)
                    throw new Exception("You are trying to install a tower, but the tower is installed!");

                IBaseTower createTower = _objectPooler.GetObject(tower,
                                                                 transform.position,
                                                                 transform.rotation);

                BaseTower = createTower;
                BaseTower.SetTower(transform.position + Vector3.back * 2);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }
        public void DeleteTower()
        {
            BaseTower.DeleteTower();
        }

        #endregion
    }
}
