using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    
    protected int _attackCounter;
    
    protected Animator _baseAnimator;
    protected Animator _weaponAnimator;

    protected PlayerAttackState _state;

    protected void Start()
    {
        _baseAnimator = transform.Find("Base").GetComponent<Animator>();
        _weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        
        gameObject.SetActive(false);
    }

    public void InitializeWeapon(PlayerAttackState state)
    {
        _state = state;
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (_attackCounter >= weaponData.movementSpeed.Length)
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
        _state.SetPlayerVelocity(weaponData.movementSpeed[_attackCounter]);
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
    
    #endregion
}
