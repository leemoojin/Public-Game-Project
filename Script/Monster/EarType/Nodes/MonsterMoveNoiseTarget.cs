using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Noise Target")]
public class MonsterMoveNoiseTarget : MoveToVector
{
    //public float moveSpeed = 10f;// SO
    public IntReference curState;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;

    public override void OnEnter()
    {
        curState.Value = (int)EarTypeMonsterState_.MoveState;
        agent.speed = baseSpeed.Value * runSpeedModifier.Value;

        base.OnEnter();
    }
}
