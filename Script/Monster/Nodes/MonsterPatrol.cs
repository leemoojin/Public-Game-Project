using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Patrol")]
public class MonsterPatrol : MoveToVector
{
    public BoolReference variableToSkip;
    public FloatReference baseSpeed;
    public FloatReference walkSpeedModifier;
    public IntReference curState;
    public IntReference monsterType;

    public Animator animator;
    //public float moveSpeed = 5f;

    public override void OnEnter()
    {
        //Debug.Log("MonsterPatrol - OnEnter()");

        //state.Value = (int)MonsterState.PatrolState;
        agent.speed = baseSpeed.Value * walkSpeedModifier.Value;

        if (monsterType.Value == 1)
        {
            curState.Value = (int)EyeTypeMonsterState.Walk;
            //Debug.Log($"MonsterPatrol - OnEnter() - curState walk : {(EyeTypeMonsterState)curState.Value}");

            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", true);
        }
        else if (monsterType.Value == 2)
        {
            curState.Value = (int)EarTypeMonsterState.Walk;
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
        if (variableToSkip.Value)
        {
            //Debug.Log("MonsterPatrol - variableToSkip()");
            return NodeResult.failure;
        }
        return base.Execute();
    }
}
