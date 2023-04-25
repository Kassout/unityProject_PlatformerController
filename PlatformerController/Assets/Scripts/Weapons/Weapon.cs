using UnityEngine;

public class Weapon : MonoBehaviour
{
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
        
        _baseAnimator.SetBool("attack", true);
        _weaponAnimator.SetBool("attack", true);
    }

    public virtual void ExitWeapon()
    {
        _baseAnimator.SetBool("attack", false);
        _weaponAnimator.SetBool("attack", false);
        
        gameObject.SetActive(false);
    }

    #region Animation Triggers

    public virtual void AnimationFinishTrigger()
    {
        _state.AnimationFinishTrigger();
    }

    #endregion
}
