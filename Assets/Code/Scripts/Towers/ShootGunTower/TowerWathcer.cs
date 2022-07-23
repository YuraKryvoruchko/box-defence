using UnityEngine;

namespace BoxDefence.Towers
{
    public class TowerWathcer
    {
        private Transform _upperTower;

        public TowerWathcer(Transform upperTower)
        {
            _upperTower = upperTower;
        }

        public void WatchTheEnemy(Transform transformTawer, Vector3 enemyPosition)
        {
            if (_upperTower != null)
            {
                _upperTower.rotation = Quaternion.Euler(transformTawer.rotation.eulerAngles.x,
                                                                  transformTawer.rotation.eulerAngles.y,
                                                                  Mathf.Atan2(enemyPosition.y - transformTawer.position.y,
                                                                              enemyPosition.x - transformTawer.position.x) * Mathf.Rad2Deg - 90);
            }
            else
            {
                Debug.Log("Upper Tower is null!");
            }
        }
    }
}
