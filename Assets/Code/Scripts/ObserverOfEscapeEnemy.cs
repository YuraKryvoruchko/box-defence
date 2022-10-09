using System;
using UnityEngine;
using AYellowpaper;

namespace BoxDefence.UI
{
    public class ObserverOfEscapeEnemy : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _gameOverPanel;
        [Space]
        [SerializeField] private int _maxPassedEnemyCount;
        [Space]
        [SerializeField] private SpawnTileObserver _spawnTileObserver;
        [SerializeField] private InterfaceReference<IPassedEnemyCounterAdapting, MonoBehaviour> 
            _passedEnemyCounter;

        private const int GAME_SPEED_ON_START = 1;
        private const int GAME_SPEED_ON_GAMEOVER = 0;

        private const string NOT_ENOUGH_ENEMIES = "Max passed enemy count less than the maximum " +
            "number of enemies passed";

        #endregion

        #region Properties

        private IPassedEnemyCounterAdapting PassedEnemyCounter { get => _passedEnemyCounter.Value; }

        #endregion

        #region Unity Methods

        private void Start()
        {
            Time.timeScale = GAME_SPEED_ON_START;
        }
        private void OnEnable()
        {
            _spawnTileObserver.OnCreateAllEnemyBase += Init;

            if(PassedEnemyCounter != null)
                PassedEnemyCounter.OnAddPassedEnemy += CoutnEnemyOnLastPoint;
        }
        private void OnDisable()
        {
            _spawnTileObserver.OnCreateAllEnemyBase -= Init;

            if (PassedEnemyCounter != null)
                PassedEnemyCounter.OnAddPassedEnemy -= CoutnEnemyOnLastPoint;
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            PassedEnemyCounter.Init();
            PassedEnemyCounter.OnAddPassedEnemy += CoutnEnemyOnLastPoint;

            int maxPassedEnemyCount = PassedEnemyCounter.GetMaxPassedEnemyCount();
            if (maxPassedEnemyCount < _maxPassedEnemyCount)
                throw new Exception(NOT_ENOUGH_ENEMIES);

            CoutnEnemyOnLastPoint();
        }
        private void CoutnEnemyOnLastPoint()
        {
            int currentPassedEnemyCoutn = PassedEnemyCounter.GetPassedEnemyCount();

            if (currentPassedEnemyCoutn >= _maxPassedEnemyCount)
                GameOver();
        }
        private void GameOver()
        {
            if (_gameOverPanel != null)
                _gameOverPanel.SetActive(true);
            else
                Debug.LogError("Game panel is null!");

            Time.timeScale = GAME_SPEED_ON_GAMEOVER;
        }

        #endregion
    }
}
