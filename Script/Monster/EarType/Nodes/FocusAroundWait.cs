using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Focus Around Wait")]
public class FocusAroundWait : Wait
{
    public IntReference state;
    public BoolReference variableToSkip;// isFocusAround
    public BoolReference isDetect;

    public override void OnEnter()
    {
        Debug.Log("FocusAroundWait - OnEnter()");
        variableToSkip.Value = true;
        state.Value = (int)EarTypeMonsterState.FocusAround;
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
