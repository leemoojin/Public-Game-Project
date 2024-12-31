using MBT;
using UnityEngine;
using UnityEngine.AI;
[AddComponentMenu("")]
[MBTNode("Example/Monster Attack")]
public class MonsterAttack : Leaf
{
    public BoolReference isWork;// 매니저로 전역으로 데이터 관리 수정
    public IntReference curState;

    public Animator animator;

    //public TransformReference target;

    public override void OnEnter()
    {        
        Debug.Log("MonsterAttack - OnEnter() - 플레이어 사망 - 게임오버");
        base.OnEnter();

        curState.Value = (int)EyeTypeMonsterState.Attack;
        Debug.Log($"MonsterAttack - OnEnter() - curState Attack : {(EyeTypeMonsterState)curState.Value}");

        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", true);

        isWork.Value = false;
    }

    public override NodeResult Execute()
    {
        return NodeResult.success;
    }
}
