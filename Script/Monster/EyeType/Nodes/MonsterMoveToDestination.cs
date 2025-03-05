using MBT;
using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move To Destination")]
public class MonsterMoveToDestination : Leaf
{
    public IntReference curState;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;
    public BoolReference haveDestination;
    public BoolReference isDetect;
    public BoolReference isOriginPosition;
    public Vector3Reference destination;

    public Animator animator;// monster
    public NavMeshAgent agent;// monster
    public float stopDistance = 1f;
    public UnitSoundSystem soundSystem;// monster

    public override void OnEnter()
    {
        EyeTypeMonsterState state = (EyeTypeMonsterState)curState.Value;
        if (!state.HasFlag(EyeTypeMonsterState.Move))
        {
            soundSystem.StopStepAudio();
            curState.Value = (int)EyeTypeMonsterState.Move;
        }
        soundSystem.PlayStepSound((EyeTypeMonsterState)curState.Value);
        animator.SetBool("Run", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);
        agent.speed = baseSpeed.Value * runSpeedModifier.Value;
        agent.isStopped = false;
        agent.SetDestination(destination.Value);
    }

    public override NodeResult Execute()
    {
        if(isDetect.Value) return NodeResult.success;

        if (soundSystem.GroundChange)
        {
            soundSystem.PlayStepSound((EyeTypeMonsterState)curState.Value);
        }

        if (agent.pathPending) return NodeResult.running;
        if (agent.hasPath) return NodeResult.running;
        if (agent.remainingDistance < stopDistance) return NodeResult.success;

        return NodeResult.failure;
    }

    public override void OnExit()
    {
        //Debug.Log($"MonsterMoveToDestination - OnExit()");
        haveDestination.Value = false;
        isOriginPosition.Value = false;
    }
}
