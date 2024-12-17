using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Patrol")]
public class MonsterPatrol : MoveToVector
{
    public BoolReference variableToSkip;
    public float moveSpeed = 5f;

    public override void OnEnter()
    {
        //Debug.Log("MonsterPatrol - OnEnter()");

        state.Value = (int)MonsterState.PatrolState;
        agent.speed = moveSpeed;
        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (variableToSkip.Value)
        {
            Debug.Log("MonsterPatrol - variableToSkip()");
            return NodeResult.failure;
        }
        return base.Execute();
    }
}
