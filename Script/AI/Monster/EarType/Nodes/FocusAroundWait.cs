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

    public Monster monster;

    private float _timer;

    public override void OnEnter()
    {
        monster.Sound.StopStepAudio();
        curState.Value = (int)MonsterState.Lost;
        isDetect.Value = false;
        _timer = 0f;
        monster.SetAnimation(false, false, false, true);
        //Debug.Log($"FocusAroundWait - OnEnter() - isFocusAround : {isFocusAround.Value}, isDetect : {isDetect.Value}");
    }

    public override NodeResult Execute()
    {
        //Debug.Log($"FocusAroundWait - Execute() - _timer, {_timer}");

        if (canAttack.Value || isDetect.Value)
        {
            isFocusAround.Value = false;
            return NodeResult.success;
        }

        if (_timer >= focusTime.Value)
        {
            //Debug.Log($"FocusAroundWait - Execute() - _timer, {_timer}, focusTime : {focusTime.Value}");
            isFocusAround.Value = false;
            return NodeResult.success;
        }

        _timer += this.DeltaTime;
        return NodeResult.running;
    }

    //public override void OnExit()
    //{
    //    //Debug.Log($"FocusAroundWait - OnExit()");
    //    if(_timer >= focusTime.Value) isFocusAround.Value = false;
    //}
}
