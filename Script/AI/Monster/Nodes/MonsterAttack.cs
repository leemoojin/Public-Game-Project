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
    public TransformReference self;


    public EyeTypeAttackSystem attackSystem;
    public Monster monster;
    public NpcUnit npcCache;
    private AnimatorStateInfo stateInfo;

    private bool _isNPC;
    private bool _isGameover;

    public override void OnEnter()
    {
        monster.Sound.StopStepAudio();
        monster.agent.isStopped = true;
        curState.Value = (int)MonsterState.Attack;

        if (target.Value.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isWork.Value = false;
            _isNPC = false;
            monster.SetAnimation(true, false, false, false, false);
            _isGameover = true;
            GameManager.Instance.Player.OnHitSuccess(UnitType.EyeTypeMonster);
        }
        else if (target.Value.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            //Debug.Log($"MonsterAttack - hit npc");
            isWork.Value = false;
            self.Value.LookAt(target.Value);

            if (npcCache == null) if (target.Value.gameObject.TryGetComponent<NpcUnit>(out NpcUnit npcUnit)) npcUnit.NpcStop();
            else npcCache.NpcStop();

            _isNPC = true;
            isAttacking.Value = true;
            monster.SetAnimation(false, false, false, true, false);
            _isGameover = false;
        }
        else if (target.Value.gameObject.layer == LayerMask.NameToLayer("ImportantNPC"))
        {
            isWork.Value = false;
            self.Value.LookAt(target.Value);

            if (npcCache == null) if (target.Value.gameObject.TryGetComponent<NpcUnit>(out NpcUnit npcUnit)) npcUnit.NpcStop();
            else npcCache.NpcStop();

            _isNPC = true;
            isAttacking.Value = true;
            monster.SetAnimation(false, false, false, true, false);
            _isGameover = false;
        }
    }

    public override NodeResult Execute()
    {
        if (_isGameover) return NodeResult.success;

        if (_isNPC)
        {
            stateInfo = monster.animator.GetCurrentAnimatorStateInfo(0);
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
}
