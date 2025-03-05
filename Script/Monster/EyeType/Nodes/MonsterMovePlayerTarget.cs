using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Player Target")]
public class MonsterMovePlayerTarget : MoveToTransform
{
    public TransformReference self;
    public IntReference curState;
    public IntReference monsterType;
    public FloatReference distanceToTarget;
    public FloatReference chaseRange;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;
    public FloatReference attackRange;
    public BoolReference skipValueLostToTarget;
    public BoolReference isOriginPosition;

    public Animator animator;// monster
    public UnitSoundSystem soundSystem;// monster

    public override void OnEnter()
    {
        //Debug.Log($"MonsterMovePlayerTarget - OnEnter()");

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
        if (distanceToTarget.Value <= attackRange.Value)
        {
            //Debug.Log($"MonsterMovePlayerTarget - Execute() - 공격범위");
            return NodeResult.success;
        }

        if (skipValueLostToTarget.Value && distanceToTarget.Value >= chaseRange.Value)
        {
            //Debug.Log($"MonsterMovePlayerTarget - Execute() - 탐지실패, 거리 벗어남");
            return NodeResult.failure;
        }

        //if (distanceToTarget.Value >= moveRange.Value)
        //{
        //    return NodeResult.failure;
        //}

        if (soundSystem.GroundChange)
        {
            soundSystem.PlayStepSound((EyeTypeMonsterState)curState.Value);
        }

        self.Value.LookAt(destination.Value);

        return base.Execute();
    }

    public override void OnExit()
    {
        base.OnExit();
        isOriginPosition.Value = false;
        //Debug.Log($"MonsterMovePlayerTarget - OnExit()");
        //agent.isStopped = true;
        //animator.SetBool("Run", false);
        //animator.SetBool("Walk", false);
        //animator.SetBool("Idle", true);
        //animator.SetBool("Attack", false);
    }
}
