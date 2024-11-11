using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Patrol")]
public class MonsterPatrol : MoveToVector
{
    public TransformReference variableToFailure;
    public float moveSpeed = 5f;

    public override void OnEnter()
    {
        state.Value = (int)MonsterState.PatrolState;
        agent.speed = moveSpeed;
        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (variableToFailure.Value != null) return NodeResult.failure;
        return base.Execute();
    }
}
