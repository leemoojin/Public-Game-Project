using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/EarType Monster Attack")]
public class EarTypeMonsterAttack : Leaf
{
    public IntReference curState;
    public BoolReference isWork;
    public BoolReference isNoiseDetect;


    public Animator animator;

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log($"EarTypeMonsterAttack - OnEnter() - 플레이어 사망");
        curState.Value = (int)EarTypeMonsterState.Attack;
        animator.SetBool("Idle", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Focus", false);
        //animator.SetBool("Attack", false);

        isWork.Value = false;
        isNoiseDetect.Value = false;
    }

    public override NodeResult Execute()
    {
        return NodeResult.success;
    }

    public override void OnExit()
    {
        //Debug.Log($"EarTypeMonsterAttack - OnExit() - isNoiseDetect : {isNoiseDetect.Value}, isWork : {isWork.Value}");

    }
}
