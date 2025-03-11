using MBT;
using NPC;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Services/Check Player State Service")]
public class CheckPlayerStateService : Service
{
    public IntReference curState;
    public BoolReference isCrouch;

    public Player player;
    public Animator animator;
    public CapsuleCollider capsuleCollider;

    public override void Task()
    {
        PlayerEnum.PlayerState playerState = player.CurState;

        if (player.CurState == PlayerEnum.PlayerState.CrouchState)
        {
            isCrouch.Value = true;
            curState.Value = (int)((NPCState)curState.Value | NPCState.Crouch);
            capsuleCollider.center = new Vector3(0f, 0.66f, 0f);
            capsuleCollider.height = 1.32f;
            animator.SetBool("@Crouch", true);

        }
        else
        {
            isCrouch.Value = false;
            curState.Value = (int)((NPCState)curState.Value & ~NPCState.Crouch);
            capsuleCollider.center = new Vector3(0f, 0.91f, 0f);
            capsuleCollider.height = 1.82f;
            animator.SetBool("@Crouch", false);
        }
    }
}
