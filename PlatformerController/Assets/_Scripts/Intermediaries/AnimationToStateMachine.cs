using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState attackState;
    
    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }
}
