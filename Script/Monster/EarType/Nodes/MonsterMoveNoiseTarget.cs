using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Noise Target")]
public class MonsterMoveNoiseTarget : MoveToVector
{
    public IntReference curState;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;
    public BoolReference canAttack;
    public BoolReference isFocusAround;
    //public BoolReference isNoiseDetect;
    //public BoolReference isWork;


    public Animator animator;


    public override void OnEnter()
    {
        //Debug.Log($"MonsterMoveNoiseTarget - OnEnter() - isFocusAround : {isFocusAround.Value}, isDetect : {isNoiseDetect.Value}");

        curState.Value = (int)EarTypeMonsterState.Walk;
        agent.speed = baseSpeed.Value * runSpeedModifier.Value;
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", true);
        animator.SetBool("Focus", false);
        //animator.SetBool("Attack", false);

        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (canAttack.Value) return NodeResult.success;
        return base.Execute();
    }

    public override void OnExit()
    {        
        base.OnExit();
        if (!canAttack.Value)
        {
            isFocusAround.Value = true;
        }
    
        //isNoiseDetect.Value = false;
        //isWork.Value = false;

        //Debug.Log($"MonsterMoveNoiseTarget - OnExit() - isFocusAround : {isFocusAround.Value}, isDetect : {isNoiseDetect.Value}, isWork : {isWork.Value}, canAttack : {canAttack.Value}");
    }
}
