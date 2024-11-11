using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/ArriveNoisePosition")]
public class ArriveNoisePosition : Leaf
{
    public FloatReference curDetectNoise;

    public override void OnEnter()
    {
        base.OnEnter();
        curDetectNoise.Value = 0f;
    }

    public override NodeResult Execute()
    {
        //Debug.Log("ArriveNoisePosition - Execute()");
        return NodeResult.success;
    }

}
