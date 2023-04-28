using System;
using UnityEngine;

public abstract class CoreComponent : MonoBehaviour, ILogicUpdate
{
    #region Fields

    protected Core _core;

    #endregion

    #region MonoBehaviour

    protected virtual void Awake()
    {
        _core = transform.parent.GetComponent<Core>();

        if (!_core)
        {
            Debug.LogError("There is no Core on the parent.");
        }
        
        _core.AddComponent(this);
    }

    #endregion

    #region Virtual

    public virtual void LogicUpdate() {}

    #endregion
}
