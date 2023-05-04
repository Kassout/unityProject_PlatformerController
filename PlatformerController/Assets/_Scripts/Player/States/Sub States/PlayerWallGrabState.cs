using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 _holdPosition;
    
    public PlayerWallGrabState(Player player, string animationBoolName) : base(player, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();

        _holdPosition = _player.transform.position;
        
        HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isExitingState)
        {
            HoldPosition();
            
            if (_yInput > 0)
            {
                _stateMachine.ChangeState(_player.WallClimbState);
            }
            else if (_yInput < 0 || !_grabInput)
            {
                _stateMachine.ChangeState(_player.WallSlideState);
            }   
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    private void HoldPosition()
    {
        _player.transform.position = _holdPosition;
        
        Movement.SetVelocityX(0f);
        Movement.SetVelocityY(0f);
    }
}
