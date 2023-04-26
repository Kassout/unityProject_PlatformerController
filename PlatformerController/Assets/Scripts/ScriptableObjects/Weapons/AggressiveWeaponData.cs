using UnityEngine;

[CreateAssetMenu(fileName = "newAggressiveWeaponData", menuName = "Data/Weapon Data/Aggressive Weapon")]
public class AggressiveWeaponData : WeaponData
{
    [Header("Aggressive Weapon Data")] 
    [SerializeField] private WeaponAttackDetails[] attackDetails;

    public WeaponAttackDetails[] AttackDetails => attackDetails;

    private void OnEnable()
    {
        AmountOfAttacks = attackDetails.Length;
        MovementSpeed = new float[AmountOfAttacks];

        for (int i = 0; i < AmountOfAttacks; i++)
        {
            MovementSpeed[i] = attackDetails[i].movementSpeed;
        }
    }
}
