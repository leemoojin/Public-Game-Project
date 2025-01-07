using MBT;
using UnityEngine;

// delete

public class CheckMonsterState : Service
{

    public IntReference state;
    public EarTypeMonsterState_ curState;

    public override void OnEnter()
    {
        //Debug.Log("CheckMonsterState - OnEnter()");
        base.OnEnter();
    }

    public override void Task()
    {
        curState = (EarTypeMonsterState_)state.Value;
    }
}
