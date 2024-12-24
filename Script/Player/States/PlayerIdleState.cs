using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.CurState = PlayerEnum.PlayerState.IdleState;
        //Debug.Log("PlayerIdleState - Enter()");
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("PlayerIdleState - Exit()");
    }

    public override void Update()
    {
        base.Update();

    }

    protected override void OnMovementStarted(InputAction.CallbackContext context)
    {
        base.OnMovementStarted(context);

        if (context.ReadValue<Vector2>() != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.WalkState);
        }
    }

    protected override void OnCrouchStarted(InputAction.CallbackContext context)
    {
        base.OnCrouchStarted(context);
        stateMachine.ChangeState(stateMachine.CrouchState);
    }
}
