using System;
using UnityEngine;
using BoxDefence.Towers;

public class ReturedTower : MonoBehaviour
{
    public static event Action<Tower> OnPickTower;

    public void PickTower(Tower tower)
    {
        OnPickTower?.Invoke(tower);
    }
}
