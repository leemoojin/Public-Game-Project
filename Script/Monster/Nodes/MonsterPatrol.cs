using MBT;
using NPC;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Patrol")]
public class MonsterPatrol : MoveToVector
{
    public BoolReference variableToSkip;//isDetect
    public FloatReference baseSpeed;
    public FloatReference walkSpeedModifier;
    public IntReference curState;
    public IntReference monsterType;
    public BoolReference canAttack;

    public Animator animator;
    public UnitSoundSystem soundSystem;
    //public float moveSpeed = 5f;

    public override void OnEnter()
    {
        //Debug.Log("MonsterPatrol - OnEnter()");

        //state.Value = (int)MonsterState.PatrolState;
        agent.speed = baseSpeed.Value * walkSpeedModifier.Value;

        if (monsterType.Value == 1)
        {
            //Debug.Log($"MonsterPatrol - OnEnter() - curState walk : {(EyeTypeMonsterState)curState.Value}");

            EyeTypeMonsterState state = (EyeTypeMonsterState)curState.Value;
            if (!state.HasFlag(EyeTypeMonsterState.Walk))
            {
                soundSystem.StopStepAudio();
                curState.Value = (int)EyeTypeMonsterState.Walk;
            }
            soundSystem.PlayStepSound((EyeTypeMonsterState)curState.Value);
            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", true);
        }
        else if (monsterType.Value == 2)
        {
            EarTypeMonsterState state = (EarTypeMonsterState)curState.Value;
            if (!state.HasFlag(EarTypeMonsterState.Walk))
            {
                soundSystem.StopStepAudio();
                curState.Value = (int)EarTypeMonsterState.Walk;
            }
            soundSystem.PlayStepSound((EarTypeMonsterState)curState.Value);
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
            animator.SetBool("Focus", false);
            //animator.SetBool("Attack", false);
        }

        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (variableToSkip.Value || canAttack.Value)
        {
            //Debug.Log("MonsterPatrol - variableToSkip()");
            return NodeResult.failure;
        }

        if (monsterType.Value == 1 && soundSystem.GroundChange) soundSystem.PlayStepSound((EyeTypeMonsterState)curState.Value);
        else if (monsterType.Value == 2 && soundSystem.GroundChange) soundSystem.PlayStepSound((EarTypeMonsterState)curState.Value);

        return base.Execute();
    }

}
