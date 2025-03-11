using MBT;
using NPC;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/NPC Run")]
public class NPCRun : NPCMoveToTransform
{
    public BoolReference isSkipValue;

    public Animator animator;
    public UnitSoundSystem soundSystem;


    public override void OnEnter()
    {
        //if (animator.GetBool("Run")) return;
        base.OnEnter();

        if (curState.Value != (int)NPCState.Run)
        {
            curState.Value = (int)NPCState.Run;
            soundSystem.StopStepAudio();
        }         
        soundSystem.PlayStepSound((NPCState)curState.Value);
        animator.SetBool("@Crouch", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", true);
    }

    public override NodeResult Execute()
    {
        if (!isSkipValue.Value) return NodeResult.success;

        if (distanceToplayer.Value < distance.Value)
        {
            return NodeResult.success;
        }

        if (soundSystem.GroundChange)
        {
            soundSystem.PlayStepSound((NPCState)curState.Value);
        }

        return base.Execute();
    }
}
