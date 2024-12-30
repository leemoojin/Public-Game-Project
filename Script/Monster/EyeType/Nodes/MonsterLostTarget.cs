using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Lost Target")]
public class MonsterLostTarget : Leaf
{
    public TransformReference target;

    public override void OnEnter()
    {
        base.OnEnter();
        target.Value = null;
    }

    public override NodeResult Execute()
    {
        return NodeResult.success;
    }
}
