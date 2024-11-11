using System;
using UnityEngine;
public enum States
{
    IdleState,
    WalkState,
    RunState,
    CrouchState
}

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    

    // 플레이어 상태
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerCrouchState CrouchState { get; private set; }

    // 상태 전환 조건
    public bool PressCrouchBtn { get; set; } = false;

    // 현재 상태
    public bool IsMoving { get; set; } = false;


    // 상호작용

    public float MovementSpeed { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float OriginHeight { get; private set; }

    public PlayerStateMachine(Player player)
    {
        Player = player;
        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        CrouchState = new PlayerCrouchState(this);

        MovementSpeed = player.Data.GroundData.BaseSpeed;
        OriginHeight = player.transform.localScale.y;
    }

    public override void ChangeState(IState state)
    {
        base.ChangeState(state);

        string tempString = state.ToString().Replace("Player", "");
        Player.CurState = (States)Enum.Parse(typeof(States), tempString);
    }
}