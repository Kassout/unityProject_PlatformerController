using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]
public class RangedAttackStateData : ScriptableObject
{
    public float projectileDamage = 10f;
    public float projectileSpeed = 12f;
    public float projectileTravelDistance = 8f;
    public float stunDamage = 1f;
    
    public Vector2 knockBackAngle = Vector2.one;
    public float knockBackStrength = 10f;
    
    public GameObject projectile;
}
