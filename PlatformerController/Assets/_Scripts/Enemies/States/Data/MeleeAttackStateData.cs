using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class MeleeAttackStateData : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attackDamage = 10f;
    public float stunDamage = 1f;

    public Vector2 knockBackAngle = Vector2.one;
    public float knockBackStrength = 10f;
    
    public LayerMask whatIsPlayer;
}
