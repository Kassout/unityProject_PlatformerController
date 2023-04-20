using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class EntityData : ScriptableObject
{
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;

    public float minAggroDistance = 3f;
    public float maxAggroDistance = 4f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
