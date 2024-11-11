using MBT;
using UnityEngine;

public enum MonsterState
{
    IdleState,
    PatrolState,
    MoveState
}

[AddComponentMenu("")]
[MBTNode("Example/Check Monster State")]
public class CheckMonsterState : Service
{

    public IntReference state;
    public MonsterState curState;

    public override void Task()
    {
        curState = (MonsterState)state.Value;
    }
}
