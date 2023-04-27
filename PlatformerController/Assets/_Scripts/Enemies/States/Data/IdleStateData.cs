using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle State")]
public class IdleStateData : ScriptableObject
{
    public float minIdleTime = 1f;
    public float maxIdleTime = 2f;
}
