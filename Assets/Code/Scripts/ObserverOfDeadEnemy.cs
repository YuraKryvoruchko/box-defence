using UnityEngine;
using AYellowpaper;

namespace BoxDefence.UI
{
    public class ObserverOfDeadEnemy : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _winPanel;
        [Space]
        [SerializeField] private InterfaceReference<IDeadEnemyCounting, 
            MonoBehaviour> _deadEnemyCounting;

        private int _maxEnemyCount = 0;

        #endregion

        #region Properties

        private IDeadEnemyCounting DeadEnemyCounting { get => _deadEnemyCounting.Value; }

        #endregion

        #region 

        private void Awake()
        {
            _maxEnemyCount = DeadEnemyCounting.GetMaxDeadEnemyCount();
        }
        private void OnEnable()
        {
            
        }
        private void OnDisable()
        {
            
        }

        #endregion

        #region Public Method

        public void OnWin()
        {
            if (_winPanel != null)
                _winPanel.SetActive(true);
            else
                Debug.LogError("Win panel is null!");
        }

        #endregion
    }
}
