using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : PlayerGroundState
{
    public PlayerRunState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("PlayerRunState - Enter()");
        stateMachine.Player.CurState = PlayerEnum.PlayerState.RunState;
        stateMachine.MovementSpeedModifier = stateMachine.Player.Data.GroundData.RunSpeedModifier;
        //Debug.Log($"PlayerRunState - Enter() - MovementSpeedModifier : {stateMachine.MovementSpeedModifier}");
        stateMachine.Player.SumNoiseAmount = 12f;
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("PlayerRunState - Exit()");
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.Player.Input.Player.Movement.ReadValue<Vector2>() != Vector2.zero) stateMachine.Player.FootstepsSystem.PlayStepSound(stateMachine.Player.CurState);
    }

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        base.OnRunCanceled(context);
        //Debug.Log("PlayerRunState - OnRunCanceled()");

        if (stateMachine.PressCrouchBtn)
        {
            stateMachine.ChangeState(stateMachine.CrouchState);
            return;
        }

        if (stateMachine.Player.Input.Player.Movement.ReadValue<Vector2>() == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.WalkState);
        }
    }
}
