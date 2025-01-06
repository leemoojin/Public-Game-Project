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
    public CapsuleCollider collider;

    public override void Task()
    {
        PlayerEnum.PlayerState playerState = player.CurState;

        if (player.CurState == PlayerEnum.PlayerState.CrouchState)
        {
            isCrouch.Value = true;
            curState.Value = (int)((NPCState)curState.Value | NPCState.Crouch);
            collider.center = new Vector3(0f, 2.6f, 0f);
            collider.height = 5.2f;
            animator.SetBool("@Crouch", true);

        }
        else
        {
            isCrouch.Value = false;
            curState.Value = (int)((NPCState)curState.Value & ~NPCState.Crouch);
            collider.center = new Vector3(0f, 3.5f, 0f);
            collider.height = 7f;
            animator.SetBool("@Crouch", false);
        }
    }
}
