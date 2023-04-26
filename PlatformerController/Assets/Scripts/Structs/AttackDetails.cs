using System;
using UnityEngine;

public struct AttackDetails
{
    public Vector2 position;
    public float damageAmount;
    public float stunDamageAmount;
}

[Serializable]
public struct WeaponAttackDetails
{
    public string attackName;
    public float movementSpeed;
    public float damageAmount;
}
