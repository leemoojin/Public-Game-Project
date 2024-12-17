using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Wait")]
public class MonsterWait : Wait
{
    public IntReference state;
    public BoolReference variableToSkip;


    public override void OnEnter()
    {
        //Debug.Log("MonsterWait - OnEnter()");
        state.Value = (int)MonsterState.IdleState;
        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (variableToSkip.Value)
        {
            Debug.Log("MonsterWait - Execute() - variableToSkip");
            return NodeResult.failure; 
        }
        return base.Execute();
    }
}
