using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerGroundState
{
    public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("PlayerWalkState - Enter()");
        stateMachine.Player.CurState = PlayerEnum.PlayerState.WalkState;
        //stateMachine.MovementSpeedModifier = 1;
        stateMachine.MovementSpeedModifier = stateMachine.Player.Data.GroundData.WalkSpeedModifier;
        stateMachine.Player.SumNoiseAmount = 6f;
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("PlayerWalkState - Exit()");
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Player.Input.Player.Movement.ReadValue<Vector2>() != Vector2.zero) stateMachine.Player.FootstepsSystem.PlayStepSound(stateMachine.Player.CurState);
    }

    protected override void OnCrouchStarted(InputAction.CallbackContext context)
    {
        base.OnCrouchStarted(context);
        stateMachine.ChangeState(stateMachine.CrouchState);
    }

    protected override void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        base.OnCrouchCanceled(context);
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        base.OnMovementCanceled(context);
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
