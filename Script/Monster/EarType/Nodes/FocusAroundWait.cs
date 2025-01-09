using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Focus Around Wait")]
public class FocusAroundWait : Wait
{
    public IntReference curState;
    public IntReference state;
    public BoolReference variableToSkip;// isFocusAround
    public BoolReference isDetect;

    public Animator animator;

    public override void OnEnter()
    {
        Debug.Log("FocusAroundWait - OnEnter()");
        if (!variableToSkip.Value) 
        {
            curState.Value = (int)EarTypeMonsterState.Focus;
            variableToSkip.Value = true;
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("Focus", true);
            //animator.SetBool("Attack", false);
        }

        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (!variableToSkip.Value)
        {
            Debug.Log("FocusAroundWait - Execute() - variableToSkip");
            return NodeResult.success;
        }
        return base.Execute();
    }

    public override void OnExit()
    {
        base.OnExit();

        if (variableToSkip.Value)
        {
            Debug.Log("FocusAroundWait - OnExit()");
            isDetect.Value = false;
            variableToSkip.Value = false;
        }
    }
}
