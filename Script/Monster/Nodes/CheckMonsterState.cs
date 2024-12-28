using MBT;
using UnityEngine;

// delete

public class CheckMonsterState : Service
{

    public IntReference state;
    public MonsterState curState;

    public override void OnEnter()
    {
        //Debug.Log("CheckMonsterState - OnEnter()");
        base.OnEnter();
    }

    public override void Task()
    {
        curState = (MonsterState)state.Value;
    }
}
