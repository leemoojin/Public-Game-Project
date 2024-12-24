using MBT;
using NPC;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/NPC Move")]
public class NPCMove : NPCMoveToTransform
{
    public GameObjectReference playerObject;
    public Animator animator;

    private Player _player;

    public override void OnAllowInterrupt()
    {
        base.OnAllowInterrupt();
        _player = playerObject.Value.GetComponent<Player>();
    }

    public override void OnEnter()
    {

        base.OnEnter();

        if (curState.Value == (int)NPCState.Move && animator.GetBool("Walk")) return;
        curState.Value = (int)NPCState.Move;
        animator.SetBool("Idle", false);
        animator.SetBool("Run", false);
        animator.SetBool("Walk", true);
    }

    public override NodeResult Execute()
    {
        if (distanceToplayer.Value > distance.Value)
        {
            return NodeResult.success;
        }

        if (_player.CurState == PlayerEnum.PlayerState.CrouchState)
        {
            //Debug.Log("NPCMove - 플레이어 앉음");

            curState.Value = (int)(NPCState.Crouch | (NPCState)curState.Value);
            Debug.Log((NPCState)curState.Value);
        }

        return base.Execute();
    }
}
