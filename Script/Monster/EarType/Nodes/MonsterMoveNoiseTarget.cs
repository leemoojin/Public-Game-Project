using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Noise Target")]
public class MonsterMoveNoiseTarget : MoveToVector
{
    public float moveSpeed = 10f;// SO

    public override void OnEnter()
    {
        state.Value = (int)MonsterState.MoveState;
        agent.speed = moveSpeed;

        base.OnEnter();
    }
}
