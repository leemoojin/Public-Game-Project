using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/NPC Move To Transform")]
public class NPCMoveToTransform : MoveToTransform
{
    public FloatReference nearDistance;


    public override void OnEnter()
    {
        stopDistance = nearDistance.Value;
        base.OnEnter();
    }
}
