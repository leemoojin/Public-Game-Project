using MBT;
using UnityEngine;
using UnityEngine.AI;
[AddComponentMenu("")]
[MBTNode("Example/Monster Move To Stand Position")]
public class MonsterMoveToStandPosition : Leaf
{
    public IntReference curState;
    public FloatReference baseSpeed;
    public FloatReference walkSpeedModifier;
    public BoolReference isDetect;
    public Vector3Reference originPosition;

    public Animator animator;// monster
    public UnitSoundSystem soundSystem;// monster
    public NavMeshAgent agent;// monster
    public float stopDistance = 2f;

    public override void OnEnter()
    {
        EyeTypeMonsterState state = (EyeTypeMonsterState)curState.Value;
        if (!state.HasFlag(EyeTypeMonsterState.Walk))
        {
            soundSystem.StopStepAudio();
        }
        curState.Value = (int)EyeTypeMonsterState.Walk;
        soundSystem.PlayStepSound((EyeTypeMonsterState)curState.Value);
        agent.speed = baseSpeed.Value * walkSpeedModifier.Value;
        animator.SetBool("Run", false);
        animator.SetBool("Walk", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);
        if (agent.isStopped) agent.isStopped = false;
        agent.SetDestination(originPosition.Value);
    }

    public override NodeResult Execute()
    {
        if(isDetect.Value) return NodeResult.success;
        if (soundSystem.GroundChange) soundSystem.PlayStepSound((EyeTypeMonsterState)curState.Value);

        if (agent.pathPending) return NodeResult.running;
        if (agent.hasPath) return NodeResult.running;

        if (agent.remainingDistance < stopDistance)
        {
            return NodeResult.success;
        }
        return NodeResult.failure;
    }

    public override void OnExit()
    {
        curState.Value = (int)EyeTypeMonsterState.Idle;
        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Attack", false);
        soundSystem.StopStepAudio();
        agent.isStopped = true;
    }
}
