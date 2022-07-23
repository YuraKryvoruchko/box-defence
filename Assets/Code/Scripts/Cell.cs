using UnityEngine;
using BoxDefence.Towers;

public class Cell : MonoBehaviour
{
    [SerializeField] private bool _towerSet = false;

    private Tower _tower;

    public void SetTower(Tower tower)
    {
        if (_towerSet == true)
            return;

        Tower createTower = Instantiate(tower, transform, true);

        _tower = createTower;
        _tower.SetTower(transform.position + Vector3.back * 2);

        _towerSet = true;
    }
}
