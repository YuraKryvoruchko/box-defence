using UnityEngine;
using AYellowpaper;

namespace BoxDefence.UI
{
    public class ObserverOfEscapeEnemy : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _gameOverPanel;
        [Space]
        [SerializeField] private int _maxCountEnemyOnLastPoint = 1;
        [SerializeField] private InterfaceReference<IPassedEnemyCounting, MonoBehaviour> _passedEnemyCounter;

        #endregion

        #region Properties

        private IPassedEnemyCounting PassedEnemyCounter { get => _passedEnemyCounter.Value; }

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            PassedEnemyCounter.OnAddPassedEnemy += CoutnEnemyOnLastPoint;
        }
        private void OnDisable()
        {
            PassedEnemyCounter.OnAddPassedEnemy -= CoutnEnemyOnLastPoint;
        }

        #endregion

        #region Private Methods

        private void CoutnEnemyOnLastPoint()
        {
            int currentPassedEnemyCoutn = PassedEnemyCounter.GetPassedEnemyCount();
            int maxPassedEnemyCoutn = PassedEnemyCounter.GetMaxPassedEnemyCount();

            if (currentPassedEnemyCoutn >= maxPassedEnemyCoutn)
                GameOver();
        }
        private void GameOver()
        {
            if (_gameOverPanel != null)
                _gameOverPanel.SetActive(true);
            else
                Debug.LogError("Game panel is null!");
        }

        #endregion
    }
}
