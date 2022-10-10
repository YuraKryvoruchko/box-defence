using System;
using UnityEngine;
using AYellowpaper;
using BoxDefence.Enumerating;

namespace BoxDefence.UI
{
    public class ObserverOfLostEnemy : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _winPanel;
        [Space]
        [SerializeField] private SpawnTileObserver _spawnTileObserver;
        [SerializeField] private InterfaceReference<IDeadEnemyCounterAdapting, 
            MonoBehaviour> _deadEnemyCounting;
        [SerializeField] private InterfaceReference<IPassedEnemyCounterAdapting,
            MonoBehaviour> _passedEnemyCounting;

        private const int GAME_SPEED_ON_START = 1;
        private const int GAME_SPEED_ON_WIN = 0;

        #endregion

        #region Properties

        private IDeadEnemyCounterAdapting DeadEnemyCounting { get => _deadEnemyCounting.Value; }
        private IPassedEnemyCounterAdapting PassedEnemyCounting { get => _passedEnemyCounting.Value; }

        #endregion

        #region Unity Methods

        private void Start()
        {
            Time.timeScale = GAME_SPEED_ON_START;
        }
        private void OnEnable()
        {
            _spawnTileObserver.OnCreateAllEnemyBase += Init;

            if (DeadEnemyCounting == null || PassedEnemyCounting == null)
                throw new Exception();

            DeadEnemyCounting.OnAddDeadEnemy += TryOpenWinPanel;
            PassedEnemyCounting.OnAddPassedEnemy += TryOpenWinPanel;
        }
        private void OnDisable()
        {
            _spawnTileObserver.OnCreateAllEnemyBase -= Init;

            if (DeadEnemyCounting == null || PassedEnemyCounting == null)
                throw new Exception();

            DeadEnemyCounting.OnAddDeadEnemy -= TryOpenWinPanel;
            PassedEnemyCounting.OnAddPassedEnemy -= TryOpenWinPanel;
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            DeadEnemyCounting.Init();
            PassedEnemyCounting.Init();

            DeadEnemyCounting.OnAddDeadEnemy += TryOpenWinPanel;
            PassedEnemyCounting.OnAddPassedEnemy += TryOpenWinPanel;

            TryOpenWinPanel();
        }
        private void OpenWinPanel()
        {
            if (_winPanel != null)
                _winPanel.SetActive(true);
            else
                Debug.LogError("Win panel is null!");

            Time.timeScale = GAME_SPEED_ON_WIN;
        }
        private void TryOpenWinPanel()
        {
            int maxDeadEnemyCount = DeadEnemyCounting.GetMaxDeadEnemyCount();
            int lostEnemyCount = CountLostEnemy();

            if (lostEnemyCount >= maxDeadEnemyCount)
                OpenWinPanel();
        }
        private int CountLostEnemy()
        {
            int maxDeadEnemyCount = DeadEnemyCounting.GetMaxDeadEnemyCount();
            int deadEnemyCount = DeadEnemyCounting.GetDeadEnemyCount();
            int passedEnemyCount = PassedEnemyCounting.GetPassedEnemyCount();

            int lostEnemyCount = deadEnemyCount + passedEnemyCount;

            return lostEnemyCount;
        }

        #endregion
    }
}
