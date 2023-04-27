using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Player Detected State")]
public class PlayerDetectedStateData : ScriptableObject
{
    [FormerlySerializedAs("actionTime")] public float longRangeActionTime = 1.5f;
}
