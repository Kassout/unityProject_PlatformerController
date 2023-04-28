using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class EntityData : ScriptableObject
{
    public float maxHealth = 30f;

    public float damageHopSpeed = 3f;
    
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;



    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;
    


    public GameObject hitParticle;

    public LayerMask whatIsGround;

}
