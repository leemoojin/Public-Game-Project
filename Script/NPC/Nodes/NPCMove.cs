using MBT;
using NPC;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/NPC Move")]
public class NPCMove : NPCMoveToTransform
{
    public BoolReference isSkipValue;

    public Animator animator;
    public UnitSoundSystem soundSystem;


    public override void OnEnter()
    {        
        base.OnEnter();
        if (((NPCState)curState.Value & NPCState.Move) != NPCState.Move) curState.Value = (int)((NPCState)curState.Value | NPCState.Move);
        if (((NPCState)curState.Value & NPCState.Run) == NPCState.Run)
        {
            curState.Value = (int)((NPCState)curState.Value & ~NPCState.Run);
            soundSystem.StopStepAudio();
        }
        //Debug.Log($"NPCMove - OnEnter() - {(NPCState)curState.Value}");
        //if (animator.GetBool("Walk")) return;        
        soundSystem.PlayStepSound((NPCState)curState.Value);
        animator.SetBool("Idle", false);
        animator.SetBool("Run", false);
        animator.SetBool("Walk", true);        
    }

    public override NodeResult Execute()
    {
        if (!isSkipValue.Value) return NodeResult.success;

        if (distanceToplayer.Value > distance.Value)
        {
            return NodeResult.success;
        }

        if (soundSystem.GroundChange)
        {
            soundSystem.PlayStepSound((NPCState)curState.Value);
        }

        //if (_player.CurState == PlayerEnum.PlayerState.CrouchState)
        //{
        //    //Debug.Log("NPCMove - 플레이어 앉음");

        //    curState.Value = (int)(NPCState.Crouch | (NPCState)curState.Value);
        //    Debug.Log((NPCState)curState.Value);
        //}

        return base.Execute();
    }
}
