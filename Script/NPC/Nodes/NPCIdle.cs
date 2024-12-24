using MBT;
using NPC;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/NPC Idle")]
public class NPCIdle : Leaf
{
    public BoolReference isIdle;
    public IntReference curState;
    public Animator animator;


    public override void OnEnter()
    {
        //Debug.Log("NPCIdle - OnEnter() - NPCIdle");
        if (curState.Value == (int)NPCState.Idle && animator.GetBool("Idle")) return;

        base.OnEnter();

        curState.Value = (int)NPCState.Idle;
        isIdle.Value = true;
        // idle 애니매이션 실행
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Idle", true);
        
    }

    public override NodeResult Execute()
    {
        return NodeResult.success;
    }
}
