using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Attack")]
public class MonsterAttack : Leaf
{
    public BoolReference isAttacking;
    public BoolReference isDetect;
    public IntReference curState;
    public TransformReference target;

    public Animator animator;
    private AnimatorStateInfo stateInfo;

    private bool _isNPC;


    public override void OnEnter()
    {        
        //Debug.Log($"MonsterAttack - OnEnter() - target : {target.Value}, layer : {target.Value.gameObject.layer}");
        base.OnEnter();

        if (target.Value.gameObject.layer == 3)
        {
            _isNPC = false;
            isAttacking.Value = true;
            curState.Value = (int)EyeTypeMonsterState.Attack;
            Debug.Log($"MonsterAttack - OnEnter() - 플레이어 사망");
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Attack", false);

        }
        else if (target.Value.gameObject.layer == 7)
        {
            target.Value.gameObject.GetComponent<NPC.NPC>().NpcStop();
            _isNPC = true;
            isAttacking.Value = true;
            curState.Value = (int)EyeTypeMonsterState.Attack;
            //Debug.Log($"MonsterAttack - OnEnter() - curState Attack : {(EyeTypeMonsterState)curState.Value}");

            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", true);
        } 
    }

    public override NodeResult Execute()
    {
        if (target.Value.gameObject.layer == 0) return NodeResult.success;

        if (_isNPC)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //Debug.Log($"MonsterAttack - Execute() - {stateInfo.IsName("Attack")}, {stateInfo.normalizedTime}");

            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
            {
                Debug.Log($"MonsterAttack - Execute() - attack end");
                isAttacking.Value = false;
                return NodeResult.success;
            }
        }

        return NodeResult.running;
    }

    public override void OnExit()
    {
        base.OnExit();

        //Debug.Log($"MonsterAttack - OnExit()");
        isAttacking.Value = false;
        isDetect.Value = false;
        target.Value = null;
    }
}
