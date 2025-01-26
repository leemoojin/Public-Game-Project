using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Focus Around Wait")]
public class FocusAroundWait : Leaf
{
    public IntReference curState;
    public BoolReference isFocusAround;
    public BoolReference isDetect;
    public BoolReference canAttack;
    public FloatReference focusTime;

    public Animator animator;

    private float _timer;

    

    public override void OnEnter()
    {       
        curState.Value = (int)EarTypeMonsterState.Focus;
        isDetect.Value = false;
        _timer = 0f;
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Focus", true);
        //animator.SetBool("Attack", false);
        //Debug.Log($"FocusAroundWait - OnEnter() - isFocusAround : {isFocusAround.Value}, isDetect : {isDetect.Value}");

    }

    public override NodeResult Execute()
    {
        //Debug.Log($"FocusAroundWait - Execute() - _timer, {_timer}");

        if (canAttack.Value || isDetect.Value || canAttack.Value)
        {
            isFocusAround.Value = false;
            return NodeResult.success;
        }


        if (_timer >= focusTime.Value)
        {
            //Debug.Log($"FocusAroundWait - Execute() - _timer, {_timer}, focusTime : {focusTime.Value}");
            return NodeResult.success;
        }

        _timer += this.DeltaTime;
        return NodeResult.running;
    }


    public override void OnExit()
    {
        //Debug.Log($"FocusAroundWait - OnExit()");

        if(_timer >= focusTime.Value) isFocusAround.Value = false;
    }
}
