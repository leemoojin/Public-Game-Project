using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Noise Target")]
public class MonsterMoveNoiseTarget : MoveToVector
{
    public IntReference curState;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;

    public Animator animator;


    public override void OnEnter()
    {
        curState.Value = (int)EarTypeMonsterState.Walk;
        agent.speed = baseSpeed.Value * runSpeedModifier.Value;
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", true);
        animator.SetBool("Focus", false);
        //animator.SetBool("Attack", false);

        base.OnEnter();
    }
}
