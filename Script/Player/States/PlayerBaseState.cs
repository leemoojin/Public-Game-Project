using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        //Debug.Log("PlayerBaseState 생성자");

        this.stateMachine = stateMachine;
        groundData = stateMachine.Player.Data.GroundData;
    }

    public virtual void Enter()
    {
        //Debug.Log("PlayerBaseState - Enter()");

        AddInputActionsCallbacks();
    }

    protected virtual void AddInputActionsCallbacks()
    {
        stateMachine.Player.Input.Player.Movement.started += OnMovementStarted;
        stateMachine.Player.Input.Player.Movement.canceled += OnMovementCanceled;
        stateMachine.Player.Input.Player.Run.started += OnRunStarted;
        stateMachine.Player.Input.Player.Run.canceled += OnRunCanceled;
        stateMachine.Player.Input.Player.Crouch.started += OnCrouchStarted;
        stateMachine.Player.Input.Player.Crouch.canceled += OnCrouchCanceled;
        stateMachine.Player.Input.Player.Interaction.started += OnInteractionStarted;
        stateMachine.Player.Input.Player.Interaction.canceled += OnInteractionCanceled;
    }

    protected virtual void OnInteractionStarted(InputAction.CallbackContext context)
    {
        //Debug.Log($"상호작용 s");
        stateMachine.Player.PlayerInteractable.OnInteracted();
    }
    protected virtual void OnInteractionCanceled(InputAction.CallbackContext context)
    {
        //Debug.Log($"상호작용 c");
        stateMachine.Player.PlayerInteractable.OffInteracted();
    }

    protected virtual void OnMovementStarted(InputAction.CallbackContext context)
    {
        //Debug.Log($"이동 s");
        stateMachine.IsMoving = true;
    }
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        //Debug.Log($"이동 c");
        stateMachine.IsMoving = false;
    }
    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        //Debug.Log($"달리기 s");
        stateMachine.ChangeState(stateMachine.RunState);
    }
    
    protected virtual void OnRunCanceled(InputAction.CallbackContext context)
    {
        //Debug.Log($"달리기 c");
    }
    protected virtual void OnCrouchStarted(InputAction.CallbackContext context)
    {
        //Debug.Log($"앉기 s");
        stateMachine.PressCrouchBtn = true;

    }
    protected virtual void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        //Debug.Log($"앉기 c");
        stateMachine.PressCrouchBtn = false;

    }

    public virtual void Exit()
    {
        //Debug.Log("PlayerBaseState - Exit()");

        RemoveInputActionsCallbacks();
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.Player.Movement.started -= OnMovementStarted;
        stateMachine.Player.Input.Player.Movement.canceled -= OnMovementCanceled;
        stateMachine.Player.Input.Player.Run.started -= OnRunStarted;
        stateMachine.Player.Input.Player.Run.canceled -= OnRunCanceled;
        stateMachine.Player.Input.Player.Crouch.started -= OnCrouchStarted;
        stateMachine.Player.Input.Player.Crouch.canceled -= OnCrouchCanceled;
        stateMachine.Player.Input.Player.Interaction.started -= OnInteractionStarted;
        stateMachine.Player.Input.Player.Interaction.canceled -= OnInteractionCanceled;
    }

    public virtual void HandleInput()
    {
    }

    public virtual void Update()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        Move(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector2 movementInput = stateMachine.Player.Input.Player.Movement.ReadValue<Vector2>();
        //Debug.Log($"PlayerBaseState - GetMovementDirection() - movementInput : {movementInput}");
        Vector3 forward = stateMachine.Player.transform.forward;
        Vector3 right = stateMachine.Player.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        return forward * movementInput.y + right * movementInput.x;
    }

    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();

        stateMachine.Player.Controller.Move
            (
                ((direction * movementSpeed) + (stateMachine.Player.ForceReceiver.Movement * stateMachine.Player.Data.GroundData.GravityModifier)) * Time.deltaTime
            );
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

   

}
