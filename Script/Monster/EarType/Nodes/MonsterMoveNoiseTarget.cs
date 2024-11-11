using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Noise Target")]
public class MonsterMoveNoiseTarget : MoveToVector
{
    public TransformReference noiseTarget;
    public float moveSpeed = 10f;

    public override void OnEnter()
    {
        //Debug.Log("MonsterMoveNoiseTarget - OnEnter()");
        destination.Value = noiseTarget.Value.position;
        state.Value = (int)MonsterState.MoveState;
        agent.speed = moveSpeed;

        base.OnEnter();
    }
}
