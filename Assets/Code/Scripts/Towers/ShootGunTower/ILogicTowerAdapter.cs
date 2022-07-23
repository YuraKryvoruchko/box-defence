using System.Collections.Generic;
using BoxDefence.AI;

namespace BoxDefence.Towers
{
    public interface ILogicTowerAdapter
    {
        Enemy CurrentEnemy { get; set; }

        List<Enemy> EnemysInShootZone { get; set; }
    }
}
