using System;

namespace BoxDefence
{
    public class EnemyCounter
    {
        private int _enemyDead = 0;
        private int _passedWaves = 0;
        private int _passedEnemy = 0;

        private int _maxWaves = 5;
        private int _maxDeadEnemys = 5;
        private int _maxPassedEnemys = 3;

        public static event Action AllWavesDestroyed;
        public static event Action NeedCountEnemyPassed;

        public EnemyCounter(int maxWaves, int maxDeadEnemys)
        {
            _maxWaves = maxWaves;
            _maxDeadEnemys = maxDeadEnemys;
        }

        public void CountWaves()
        {
            _passedWaves++;

            if (_passedWaves == _maxWaves)
                AllWavesDestroyed?.Invoke();
        }
        public void CountPassedEnemy()
        {
            _passedEnemy++;

            if (_passedEnemy == _maxPassedEnemys)
                NeedCountEnemyPassed?.Invoke();
        }
    }
}
