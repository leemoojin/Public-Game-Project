using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Player Target Stand")]
public class MonsterMovePlayerTargetStand : MoveToTransform
{
    public IntReference curState;
    public IntReference monsterType;
    public FloatReference distanceToTarget;
    public FloatReference moveRange;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;
    public FloatReference attackRange;
    public BoolReference isLostToTarget;
    public BoolReference isStandPosition;
    public BoolReference isComeBack;

    public Animator animator;
    public UnitSoundSystem soundSystem;

    public override void OnEnter()
    {
        if (monsterType.Value == 1)
        {
            //Debug.Log($"MonsterMovePlayerTarget - OnEnter() - curState run : {(EyeTypeMonsterState)curState.Value}");            
            EyeTypeMonsterState state = (EyeTypeMonsterState)curState.Value;
            if (!state.HasFlag(EyeTypeMonsterState.Run))
            {
                soundSystem.StopStepAudio();
                curState.Value = (int)EyeTypeMonsterState.Run;
            }
            soundSystem.PlayStepSound((EyeTypeMonsterState)curState.Value);
            agent.speed = baseSpeed.Value * runSpeedModifier.Value;
            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", false);
        }

        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (distanceToTarget.Value <= attackRange.Value) return NodeResult.success;

        if (isLostToTarget.Value && distanceToTarget.Value >= moveRange.Value)
        {
            //Debug.Log($"MonsterMovePlayerTarget - Execute() - Å½Áö½ÇÆÐ, °Å¸® ¹þ¾î³²");
            return NodeResult.failure;
        }

        if (soundSystem.GroundChange) soundSystem.PlayStepSound((EyeTypeMonsterState)curState.Value);

        time += Time.deltaTime;
        // Update destination every given interval
        if (time > updateInterval)
        {
            // Reset time and update destination
            time = 0;
            if (agent.isStopped) agent.isStopped = false;
            isComeBack.Value = false;
            agent.SetDestination(destination.Value.position);
        }
        // Check if path is ready
        if (agent.pathPending)
        {
            //Debug.Log("if (agent.pathPending)");
            return NodeResult.running;
        }
        // Check if there is any path (if not pending, it should be set)
        if (agent.hasPath)
        {
            return NodeResult.running;
        }
        // Check if agent is very close to destination
        if (agent.remainingDistance < stopDistance)
        {
            //Debug.Log("if (agent.remainingDistance < stopDistance)");
            return NodeResult.success;
        }

        // By default return failure
        return NodeResult.failure;
    }

    public override void OnExit()
    {
        isStandPosition.Value = false;
        soundSystem.StopStepAudio();
        base.OnExit();
    }
}
