using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]
public class RangedAttackStateData : ScriptableObject
{
    public float projectileDamage = 10f;
    public float projectileSpeed = 12f;
    public float projectileTravelDistance = 8f;
    
    public GameObject projectile;
}
