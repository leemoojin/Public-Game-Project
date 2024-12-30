using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Player Target")]
public class MonsterMovePlayerTarget : MoveToTransform
{
    public IntReference state;
    public FloatReference distanceToTarget;
    public FloatReference moveRange;

    public float moveSpeed = 10f;// SO

    public override void OnEnter()
    {
        state.Value = (int)MonsterState.MoveState;
        agent.speed = moveSpeed;
        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (distanceToTarget.Value >= moveRange.Value)
        {
            return NodeResult.failure;
        }        
        
        return base.Execute();
    }
}
