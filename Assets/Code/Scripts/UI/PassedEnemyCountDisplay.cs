using UnityEngine;
using UnityEngine.UI;
using AYellowpaper;
using BoxDefence.Enumerating;

namespace BoxDefence.UI
{
    public class PassedEnemyCountDisplay : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _text;
        [Space]
        [SerializeField] private SpawnTileObserver _spawnTileObserver;
        [Space]
        [SerializeField] private ObserverOfEscapeEnemy _observerOfEscapeEnemy;
        [SerializeField] private InterfaceReference<IPassedEnemyCounterAdapting, MonoBehaviour>
            _passedEnemyCounter;

        #endregion

        #region Properties

        private IPassedEnemyCounterAdapting PassedEnemyCounterAdapting { get =>
                _passedEnemyCounter.Value; }

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _spawnTileObserver.OnCreateAllEnemyBase += Init;

            if (PassedEnemyCounterAdapting == null)
                return;

            PassedEnemyCounterAdapting.OnAddPassedEnemy += UpdateCount;
        }
        private void OnDisable()
        {
            _spawnTileObserver.OnCreateAllEnemyBase -= Init;

            if (PassedEnemyCounterAdapting == null)
                return;

            PassedEnemyCounterAdapting.OnAddPassedEnemy -= UpdateCount;
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            PassedEnemyCounterAdapting.Init();
            PassedEnemyCounterAdapting.OnAddPassedEnemy += UpdateCount;

            UpdateCount();
        }
        private void UpdateCount()
        {
            int passedEnemyCount = PassedEnemyCounterAdapting.GetPassedEnemyCount();
            int maxPassedEnemyCount = _observerOfEscapeEnemy.GetMaxPassedEnemyCount();

            _text.text = passedEnemyCount.ToString()
                         + " / "
                         + maxPassedEnemyCount.ToString()
                         + " Passed enemy";
        }

        #endregion
    }
}
