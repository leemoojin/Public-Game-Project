using MBT;
using NPC;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/NPC Idle")]
public class NPCIdle : Leaf
{
    public IntReference curState;
    public Animator animator;

    public UnitSoundSystem soundSystem;

    public override void OnEnter()
    {
        //Debug.Log("NPCIdle - OnEnter() - NPCIdle");
        if (animator.GetBool("Idle")) return;
        base.OnEnter();

        curState.Value = (int)NPCState.Idle;
        soundSystem.StopStepAudio();
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Idle", true);
    }

    public override NodeResult Execute()
    {
        return NodeResult.success;
    }
}
