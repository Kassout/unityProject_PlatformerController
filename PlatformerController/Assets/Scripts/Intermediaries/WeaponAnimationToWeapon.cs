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

    private void AnimationStartMovementTrigger()
    {
        _weapon.AnimationStartMovementTrigger();
    }

    private void AnimationStopMovementTrigger()
    {
        _weapon.AnimationStopMovementTrigger();
    }

    private void AnimationTurnOffFlipTrigger()
    {
        _weapon.AnimationTurnOffFlipTrigger();
    }

    private void AnimationTurnOnFlipTrigger()
    {
        _weapon.AnimationTurnOnFlipTrigger();
    }

    private void AnimationActionTrigger()
    {
        _weapon.AnimationActionTrigger();
    }
}
