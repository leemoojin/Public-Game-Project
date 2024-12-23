using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/NPC Idle")]
public class NPCIdle : Leaf
{
    public override void OnEnter()
    {
        Debug.Log("NPCIdle - OnEnter() - NPCIdle");

        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        return NodeResult.success;
    }
}
