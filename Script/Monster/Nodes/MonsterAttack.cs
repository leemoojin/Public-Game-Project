using MBT;
using NPC;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Attack")]
public class MonsterAttack : Leaf
{
    public BoolReference isAttacking;
    public BoolReference isDetect;
    public BoolReference isWork;
    public IntReference curState;
    public TransformReference target;

    public Transform self;

    public Animator animator;
    private AnimatorStateInfo stateInfo;

    private bool _isNPC;
    private bool _isPlayer;

    public override void OnEnter()
    {        
        //Debug.Log($"MonsterAttack - OnEnter() - target : {target.Value}, layer : {target.Value.gameObject.layer}");
        base.OnEnter();

        if (target.Value.gameObject.layer == 3)
        {
            _isNPC = false;
            isAttacking.Value = true;
            curState.Value = (int)EyeTypeMonsterState.Idle;
            Debug.Log($"MonsterAttack - OnEnter() - 플레이어 사망");
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Attack", false);
            _isPlayer = true;
            isWork.Value = false;
        }
        else if (target.Value.gameObject.layer == 7)
        {
            LookAtTarget();

            //target.Value.gameObject.GetComponent<NPC.NPC>().NpcStop();
            NPCDay1JHS nPCDay1JHS;
            if(target.Value.gameObject.TryGetComponent<NPCDay1JHS>(out nPCDay1JHS)) nPCDay1JHS.NpcStop();

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
        //if (target.Value.gameObject.layer == 0 || _isPlayer)
        //{
        //    Debug.Log($"MonsterAttack - Execute() - skip");
        //    return NodeResult.success;
        //}

        if (_isPlayer)
        {
            //Debug.Log($"MonsterAttack - Execute() - skip");
            return NodeResult.success;
        }

        if (_isNPC)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //Debug.Log($"MonsterAttack - Execute() - {stateInfo.IsName("Attack")}, {stateInfo.normalizedTime}");

            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
            {
                //Debug.Log($"MonsterAttack - Execute() - attack end");                
                return NodeResult.success;
            }
        }

        return NodeResult.running;
    }

    private void LookAtTarget()
    {
        Vector3 direction = target.Value.position - self.position;
        self.rotation = Quaternion.LookRotation(direction.normalized);
    }

    public override void OnExit()
    {
        base.OnExit();

        //Debug.Log($"MonsterAttack - OnExit()");
        if (_isPlayer) return;
        isDetect.Value = false;
        target.Value = null;
        isAttacking.Value = false;
    }
}
