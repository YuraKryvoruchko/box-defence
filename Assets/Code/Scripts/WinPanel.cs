using UnityEngine;

namespace BoxDefence.UI
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _winPanel;

        private void OnEnable()
        {
            EnemyCounter.AllWavesDestroyed += OnWin;
        }
        private void OnDisable()
        {
            EnemyCounter.AllWavesDestroyed -= OnWin;
        }

        public void OnWin()
        {
            if (_winPanel != null)
                _winPanel.SetActive(true);
            else
                Debug.LogError("Win panel is null!");
        }
    }
}
