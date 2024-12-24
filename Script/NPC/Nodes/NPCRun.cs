using MBT;
using NPC;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/NPC Run")]
public class NPCRun : NPCMoveToTransform
{
    public Animator animator;


    public override void OnEnter()
    {
        if (curState.Value == (int)NPCState.Run && animator.GetBool("Run")) return;

        base.OnEnter();
        curState.Value = (int)NPCState.Run;
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", true);
    }

    public override NodeResult Execute()
    {
        if (distanceToplayer.Value < distance.Value)
        {
            return NodeResult.success;
        }

        return base.Execute();
    }
}
