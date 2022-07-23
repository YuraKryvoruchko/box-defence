using UnityEngine;
using BoxDefence.AI;

namespace BoxDefence.UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;
        [Space]
        [SerializeField] private int _maxCountEnemyOnLastPoint = 1;

        private int _countEnemyOnLastPoint = 0;

        private void OnEnable()
        {
            Enemy.OnLastPoint += CoutnEnemyOnLastPoint;
        }
        private void OnDisable()
        {
            Enemy.OnLastPoint -= CoutnEnemyOnLastPoint;
        }

        public void CoutnEnemyOnLastPoint()
        {
            _countEnemyOnLastPoint++;

            if (_countEnemyOnLastPoint >= _maxCountEnemyOnLastPoint)
                GameOver();
        }
        public void GameOver()
        {
            if (_gameOverPanel != null)
                _gameOverPanel.SetActive(true);
            else
                Debug.LogError("Game panel is null!");
        }
    }
}
