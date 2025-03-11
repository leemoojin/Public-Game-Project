using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Patrol")]
public class MonsterPatrol : Leaf
{
    public BoolReference skipValueIsDetect;//isDetect
    public FloatReference baseSpeed;
    public FloatReference walkSpeedModifier;
    public IntReference curState;
    public IntReference monsterType;
    public BoolReference canAttack;

    public Monster monster;
    public float stopDistance = 2f;

    public override void OnEnter()
    {
        //Debug.Log("MonsterPatrol - OnEnter()");
        MonsterState state = (MonsterState)curState.Value;
        monster.agent.speed = baseSpeed.Value * walkSpeedModifier.Value;
        monster.agent.isStopped = false;

        if (monsterType.Value == 1)
        {
            if (!state.HasFlag(MonsterState.Walk))
            {
                monster.Sound.StopStepAudio();
                curState.Value = (int)MonsterState.Walk;
            }
            monster.Sound.PlayStepSound((MonsterState)curState.Value);
            monster.SetAnimation(false, true, false, false, false);
        }
        //else if (monsterType.Value == 2)
        //{
        //    EarTypeMonsterState state = (EarTypeMonsterState)curState.Value;
        //    if (!state.HasFlag(EarTypeMonsterState.Walk))
        //    {
        //        soundSystem.StopStepAudio();
        //        curState.Value = (int)EarTypeMonsterState.Walk;
        //    }
        //    soundSystem.PlayStepSound((EarTypeMonsterState)curState.Value);
        //    animator.SetBool("Idle", false);
        //    animator.SetBool("Walk", true);
        //    animator.SetBool("Run", false);
        //    animator.SetBool("Focus", false);
        //    //animator.SetBool("Attack", false);
        //}

        monster.agent.SetDestination(monster.GetPatrolPosition());
    }

    public override NodeResult Execute()
    {
        if (skipValueIsDetect.Value || canAttack.Value) return NodeResult.failure;

        if (monsterType.Value == 1 && monster.Sound.GroundChange) monster.Sound.PlayStepSound((MonsterState)curState.Value);
        //else if (monsterType.Value == 2 && soundSystem.GroundChange) soundSystem.PlayStepSound((EarTypeMonsterState)curState.Value);

        if (monster.agent.pathPending) return NodeResult.running;
        if (monster.agent.hasPath) return NodeResult.running;
        if (monster.agent.remainingDistance < stopDistance) return NodeResult.success;
        return NodeResult.failure;
    }
}
