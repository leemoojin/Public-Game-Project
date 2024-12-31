using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Noise Target")]
public class MonsterMoveNoiseTarget : MoveToVector
{
    public float moveSpeed = 10f;// SO
    public IntReference curState;

    public override void OnEnter()
    {
        curState.Value = (int)EarTypeMonsterState.MoveState;
        agent.speed = moveSpeed;

        base.OnEnter();
    }
}
