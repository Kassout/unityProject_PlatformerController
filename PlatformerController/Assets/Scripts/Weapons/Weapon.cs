using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;
    
    protected int _attackCounter;
    
    protected Core _core;
    protected Animator _baseAnimator;
    protected Animator _weaponAnimator;
    protected PlayerAttackState _state;

    protected virtual void Awake()
    {
        _baseAnimator = transform.Find("Base").GetComponent<Animator>();
        _weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        
        gameObject.SetActive(false);
    }

    public void InitializeWeapon(PlayerAttackState state, Core core)
    {
        _state = state;
        _core = core;
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (_attackCounter >= weaponData.AmountOfAttacks)
        {
            _attackCounter = 0;
        }
        
        _baseAnimator.SetBool("attack", true);
        _weaponAnimator.SetBool("attack", true);

        _baseAnimator.SetInteger("attackCounter", _attackCounter);
        _weaponAnimator.SetInteger("attackCounter", _attackCounter);
    }

    public virtual void ExitWeapon()
    {
        _baseAnimator.SetBool("attack", false);
        _weaponAnimator.SetBool("attack", false);

        _attackCounter++;
        
        gameObject.SetActive(false);
    }

    #region Animation Triggers

    public virtual void AnimationFinishTrigger()
    {
        _state.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        _state.SetPlayerVelocity(weaponData.MovementSpeed[_attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        _state.SetPlayerVelocity(0);
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        _state.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        _state.SetFlipCheck(true);
    }

    public virtual void AnimationActionTrigger()
    {
        
    }
    
    #endregion
}
