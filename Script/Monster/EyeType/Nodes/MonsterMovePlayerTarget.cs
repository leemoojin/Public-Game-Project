using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Player Target")]
public class MonsterMovePlayerTarget : MoveToTransform
{
    public IntReference curState;
    public IntReference monsterType;
    public FloatReference distanceToTarget;
    public FloatReference moveRange;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;
    public BoolReference variableToSkip;// lost to target

    public Animator animator;

    public override void OnEnter()
    {
        //Debug.Log($"MonsterMovePlayerTarget - OnEnter()");

        if (monsterType.Value == 1)
        {
            curState.Value = (int)EyeTypeMonsterState.Run;
            agent.speed = baseSpeed.Value * runSpeedModifier.Value;
            //Debug.Log($"MonsterMovePlayerTarget - OnEnter() - curState run : {(EyeTypeMonsterState)curState.Value}");

            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", false);
        }
        //else if (monsterType.Value == 2)
        //{
        //    curState.Value = (int)EarTypeMonsterState_.MoveState;
        //    agent.speed = moveSpeed;
        //}
        
        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (variableToSkip.Value)
        {
            return NodeResult.failure;
        }

        if (distanceToTarget.Value >= moveRange.Value)
        {
            return NodeResult.failure;
        }        
        
        return base.Execute();
    }

    public override void OnExit()
    {
        //Debug.Log($"MonsterMovePlayerTarget - OnExit()");
        agent.isStopped = true;
    }
}
