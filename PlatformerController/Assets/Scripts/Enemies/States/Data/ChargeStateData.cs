using UnityEngine;

[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State")]
public class ChargeStateData : ScriptableObject
{
    public float chargeSpeed = 6f;
    public float chargeTime = 2f;
}
