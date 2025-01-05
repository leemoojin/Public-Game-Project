using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Attack")]
public class MonsterAttack : Leaf
{
    public BoolReference isAttacking;
    public IntReference curState;

    public Animator animator;
    private AnimatorStateInfo stateInfo;

    //public TransformReference target;

    public override void OnEnter()
    {        
        //Debug.Log("MonsterAttack - OnEnter()");
        base.OnEnter();

        isAttacking.Value = true;
        curState.Value = (int)EyeTypeMonsterState.Attack;
        //Debug.Log($"MonsterAttack - OnEnter() - curState Attack : {(EyeTypeMonsterState)curState.Value}");

        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", true);
    }

    public override NodeResult Execute()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1.0f)
        {
            //Debug.Log($"attack end");
            isAttacking.Value = false;
            return NodeResult.success;
        }

        return NodeResult.running;
    }
}
