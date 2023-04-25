using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapon _weapon;

    private void Start()
    {
        _weapon = GetComponentInParent<Weapon>();
    }

    private void AnimationFinishTrigger()
    {
        _weapon.AnimationFinishTrigger();
    }
}
