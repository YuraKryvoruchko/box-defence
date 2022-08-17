using System;
using UnityEngine;

public class WayTarget : MonoBehaviour
{
    public bool IsFree { get; private set; }

    public Transform GetTransform()
    {
        IsFree = false;

        return transform;
    }
}
