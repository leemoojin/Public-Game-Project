using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Wait")]
public class MonsterWait : Wait
{
    public IntReference state;
    public TransformReference variableToFailure;


    public override void OnEnter()
    {
        base.OnEnter();
        state.Value = (int)MonsterState.IdleState;
    }

    public override NodeResult Execute()
    {
        if (variableToFailure.Value != null) return NodeResult.failure;
        return base.Execute();
    }
}
