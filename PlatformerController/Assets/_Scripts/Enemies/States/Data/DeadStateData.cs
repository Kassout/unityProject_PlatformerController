using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead State")]
public class DeadStateData : ScriptableObject
{
    public GameObject deathChunkParticles;
    public GameObject deathBloodParticles;
}
