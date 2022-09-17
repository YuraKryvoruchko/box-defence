using System;

namespace BoxDefence
{
    public interface IEnemyWavesCounting
    {
        event Action OnAddEnemyWaves;
        event Action OnRemoveEnemyWaves;

        int GetEnemyWavesCount();
        int GetMaxEnemyWavesCount();
    }
    public interface IDeadEnemyCounting
    {
        event Action OnAddDeadEnemy;
        event Action OnRemoveDeadEnemy;

        int GetDeadEnemyCount();
        int GetMaxDeadEnemyCount();
    }
    public interface IPassedEnemyCounting
    {
        event Action OnAddPassedEnemy;
        event Action OnRemovePassedEnemy;

        int GetPassedEnemyCount();
        int GetMaxPassedEnemyCount();
    }
    public interface IEnemyControlPointing : IPassedEnemyCounting, 
        IDeadEnemyCounting, 
        IEnemyWavesCounting
    {
    }
}
