using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/EarType Monster Attack")]
public class EarTypeMonsterAttack : Leaf
{
    public IntReference curState;

    public Animator animator;

    public override void OnEnter()
    {
        //Debug.Log("EarTypeMonsterAttack - OnEnter()");
        base.OnEnter();

        curState.Value = (int)EarTypeMonsterState.Attack;
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Focus", false);
        animator.SetBool("Attack", true);
    }

    public override NodeResult Execute()
    {
        throw new System.NotImplementedException();
    }
}
