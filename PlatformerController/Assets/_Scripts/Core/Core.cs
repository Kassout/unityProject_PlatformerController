using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core : MonoBehaviour
{
    #region Fields

    private readonly List<CoreComponent> _coreComponents = new();

    #endregion

    #region Public

    public void LogicUpdate()
    {
        foreach (CoreComponent component in _coreComponents)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!_coreComponents.Contains(component))
        {
            _coreComponents.Add(component);
        }
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var component = _coreComponents.OfType<T>().FirstOrDefault();

        if (component)
        {
            return component;
        }
        
        component = GetComponentInChildren<T>();

        if (component)
        {
            return component;
        }

        Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");

        return null;
    }

    public T GetCoreComponent<T>(out T component) where T : CoreComponent
    {
        component = GetCoreComponent<T>();
        return component;
    }

    #endregion
}
