using MBT;
using NPC;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Services/Check Player State Service")]
public class CheckPlayerStateService : Service
{
    public IntReference curState;
    public Player player;
    public BoolReference isCrouch;
    public Animator animator;

    public override void Task()
    {
        PlayerEnum.PlayerState playerState = player.CurState;

        if (player.CurState == PlayerEnum.PlayerState.CrouchState)
        {
            isCrouch.Value = true;
            curState.Value = (int)((NPCState)curState.Value | NPCState.Crouch);
            animator.SetBool("@Crouch", true);

        }
        else
        {
            isCrouch.Value = false;
            curState.Value = (int)((NPCState)curState.Value & ~NPCState.Crouch);
            animator.SetBool("@Crouch", false);
        }
    }
}
