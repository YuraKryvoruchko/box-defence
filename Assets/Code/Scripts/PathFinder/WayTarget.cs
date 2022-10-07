using UnityEngine;

public class WayTarget : MonoBehaviour
{
    #region Properties

    public bool IsFree { get; private set; }

    #endregion

    #region Unity Methods

    private void Start()
    {
        Debug.Log("Create target: " + Time.realtimeSinceStartup);  
    }

    #endregion

    #region Public Methods

    public Transform GetTransform()
    {
        IsFree = false;

        return transform;
    }

    #endregion
}
