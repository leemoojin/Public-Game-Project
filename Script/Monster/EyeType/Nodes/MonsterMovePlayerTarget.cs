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

    public float moveSpeed = 10f;// SO
    public Animator animator;


    public override void OnEnter()
    {
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
        else if (monsterType.Value == 2)
        {
            curState.Value = (int)EarTypeMonsterState_.MoveState;
            agent.speed = moveSpeed;
        }
        
        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (distanceToTarget.Value >= moveRange.Value)
        {
            return NodeResult.failure;
        }        
        
        return base.Execute();
    }

    public override void OnExit()
    {
        agent.isStopped = true;
    }
}
