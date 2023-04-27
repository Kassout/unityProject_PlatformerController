using UnityEngine;

public abstract class CoreComponent : MonoBehaviour, ILogicUpdate
{
    protected Core _core;

    protected virtual void Awake()
    {
        _core = transform.parent.GetComponent<Core>();

        if (!_core)
        {
            Debug.LogError("There is no Core on the parent.");
        }
        
        _core.AddComponent(this);
    }

    public virtual void LogicUpdate() {}
}
