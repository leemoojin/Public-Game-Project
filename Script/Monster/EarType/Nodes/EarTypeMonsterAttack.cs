using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/EarType Monster Attack")]
public class EarTypeMonsterAttack : Leaf
{
    public IntReference curState;
    public BoolReference isWork;

    public Animator animator;

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("EarTypeMonsterAttack - OnEnter() - 플레이어 사망");
        curState.Value = (int)EarTypeMonsterState.Attack;
        animator.SetBool("Idle", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Focus", false);
        //animator.SetBool("Attack", false);
        isWork.Value = false;
    }

    public override NodeResult Execute()
    {
        return NodeResult.success;
    }
}
