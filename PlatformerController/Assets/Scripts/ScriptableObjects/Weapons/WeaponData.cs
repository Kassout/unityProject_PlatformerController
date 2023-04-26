using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
public class WeaponData : ScriptableObject
{
    public int AmountOfAttacks { get; protected set; }
    public float[] MovementSpeed { get; protected set; }
}
