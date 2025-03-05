using MBT;
using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("")]
[MBTNode("Example/Monster Rest")]
public class MonsterRest : Wait
{
    public IntReference curState;
    public BoolReference isDetect;

    public Animator animator;// monster
    public NavMeshAgent agent;// monster
    public UnitSoundSystem soundSystem;// monster

    public override void OnEnter()
    {
        //Debug.Log($"MonsterRest - OnEnter");
        EyeTypeMonsterState state = (EyeTypeMonsterState)curState.Value;
        if (!state.HasFlag(EyeTypeMonsterState.Idle))
        {
            agent.isStopped = true;
            soundSystem.StopStepAudio();
        }
        curState.Value = (int)EyeTypeMonsterState.Idle;
        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Attack", false);
        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (isDetect.Value) return NodeResult.success;
        return base.Execute();
    }
}
