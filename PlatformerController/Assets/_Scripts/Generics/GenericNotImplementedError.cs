using UnityEngine;

public static class GenericNotImplementedError<T> where T : Object
{
    public static T TryGet(T value, string name)
    {
        if (value)
        {
            return value;
        }
        
        Debug.LogError($"{typeof(T)} not implemented on {name}");
        return null;
    }
}
