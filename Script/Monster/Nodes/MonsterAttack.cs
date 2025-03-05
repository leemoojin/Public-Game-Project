using MBT;
using NPC;
using UnityEngine;
using static UnitEnum;

[AddComponentMenu("")]
[MBTNode("Example/Monster Attack")]
public class MonsterAttack : Leaf
{
    public BoolReference isAttacking;
    public BoolReference isDetect;
    public BoolReference isWork;
    public IntReference curState;
    public TransformReference target;

    public EyeTypeAttackSystem attackSystem;
    public Transform self;// monster
    public UnitSoundSystem soundSystem;// monster
    public Animator animator;// monster
    private AnimatorStateInfo stateInfo;

    private bool _isNPC;
    private bool _isGameover;

    public override void OnEnter()
    {
        soundSystem.StopStepAudio();
        //Debug.Log($"MonsterAttack - OnEnter() - target : {target.Value}, layer : {target.Value.gameObject.layer}");
        base.OnEnter();

        if (target.Value.gameObject.layer == 3)
        {
            isWork.Value = false;
            _isNPC = false;
            isAttacking.Value = true;
            curState.Value = (int)EyeTypeMonsterState.Idle;
            Debug.Log($"MonsterAttack - OnEnter() - 플레이어 사망");
            //GameManager.Instance.Player.JumpScareManager.PlayerDead(JumpScareType.eyeTypeMonster);
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Attack", false);
            _isGameover = true;
            //isDetect.Value = false;
            GameManager.Instance.Player.OnHitSuccess(UnitType.EyeTypeMonster);
            //if (target.Value.gameObject.TryGetComponent<IAttackable>(out IAttackable attackable)) attackable.OnHitSuccess(UnitType.EyeTypeMonster);
        }
        else if (target.Value.gameObject.layer == 7)
        {
            isWork.Value = false;
            LookAtTarget();

            if (target.Value.gameObject.TryGetComponent<NpcUnit>(out NpcUnit npcUnit)) npcUnit.NpcStop();

            _isNPC = true;
            isAttacking.Value = true;
            curState.Value = (int)EyeTypeMonsterState.Attack;

            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", true);
            _isGameover = false;
            //isWork.Value = true;
        }
        else if (target.Value.gameObject.layer == 13)
        {
            isWork.Value = false;
            LookAtTarget();

            if (target.Value.gameObject.TryGetComponent<NpcUnit>(out NpcUnit npcUnit)) npcUnit.NpcStop();

            _isNPC = true;
            isAttacking.Value = true;
            curState.Value = (int)EyeTypeMonsterState.Attack;

            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", true);
            //_isGameover = true;
            _isGameover = false;
        }
    }

    public override NodeResult Execute()
    {
        //if (target.Value.gameObject.layer == 0 || _isPlayer)
        //{
        //    Debug.Log($"MonsterAttack - Execute() - skip");
        //    return NodeResult.success;
        //}

        if (_isGameover)
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
                if (attackSystem.reWork)
                {
                    isWork.Value = true;
                    isAttacking.Value = false;
                    return NodeResult.success;
                }
                else
                {
                    _isGameover = true;
                    return NodeResult.success;
                }
                
            }
        }

        return NodeResult.running;
    }

    private void LookAtTarget()
    {
        Vector3 direction = target.Value.position - self.position;
        self.rotation = Quaternion.LookRotation(direction.normalized);
    }

    //public override void OnExit()
    //{
    //    base.OnExit();

    //    Debug.Log($"MonsterAttack - OnExit()");
    //    //if (_isGameover) return;
    //    //target.Value = null;
    //    //isAttacking.Value = false;
    //    //isDetect.Value = false;
    //}
}
