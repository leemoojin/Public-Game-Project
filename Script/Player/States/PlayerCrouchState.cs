using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrouchState : PlayerGroundState
{
    public PlayerCrouchState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("PlayerCrouchState - Enter()");

        stateMachine.Player.transform.localScale = new Vector3(stateMachine.Player.transform.localScale.x, groundData.CrouchHeight, stateMachine.Player.transform.localScale.z);
        stateMachine.MovementSpeedModifier = groundData.CrouchSpeedModifier;
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("PlayerCrouchState - Exit()");

        stateMachine.Player.transform.localScale = new Vector3(stateMachine.Player.transform.localScale.x, stateMachine.OriginHeight, stateMachine.Player.transform.localScale.z);
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.Player.Input.playerActions.Movement.ReadValue<Vector2>() != Vector2.zero) stateMachine.Player.FootstepsSystem.PlayStepSound(stateMachine.Player.CurState);
    }

    protected override void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        base.OnCrouchCanceled(context);

        if (stateMachine.Player.Input.playerActions.Movement.ReadValue<Vector2>() == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.WalkState);
        }
    }
}
