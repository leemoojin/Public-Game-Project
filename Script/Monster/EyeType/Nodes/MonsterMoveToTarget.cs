using MBT;
using UnityEngine;
using UnityEngine.TestTools;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move To Target")]
public class MonsterMoveToTarget : MoveToTransform
{
    public IntReference curState;
    public IntReference monsterType;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;
    public BoolReference isDetect;
    public BoolReference haveTarget;
    public BoolReference isOriginPosition;

    public Animator animator;// monster
    public UnitSoundSystem soundSystem;// monster

    public override void OnEnter()
    {
        if (monsterType.Value == 1)
        {
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
        if (isDetect.Value) return NodeResult.success;
        if (soundSystem.GroundChange)
        {
            soundSystem.PlayStepSound((EyeTypeMonsterState)curState.Value);
        }

        return base.Execute();
    }

    public override void OnExit()
    {
        //Debug.Log($"MonsterMoveToDestination - OnExit()");
        base.OnExit();
        haveTarget.Value = false;
        isOriginPosition.Value = false;
    }
}
