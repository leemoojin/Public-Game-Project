using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move To Destination")]
public class MonsterMoveToDestination : Leaf
{
    public IntReference curState;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;
    public BoolReference haveDestination;
    public BoolReference isDetect;
    public BoolReference isOriginPosition;
    public Vector3Reference destination;

    public Monster monster;
    public float stopDistance = 1f;

    public override void OnEnter()
    {
        MonsterState state = (MonsterState)curState.Value;
        if (!state.HasFlag(MonsterState.Run))
        {
            monster.Sound.StopStepAudio();
            curState.Value = (int)MonsterState.Run;
        }
        monster.Sound.PlayStepSound((MonsterState)curState.Value);
        monster.SetAnimation(false, false, true, false, false);
        monster.agent.speed = baseSpeed.Value * runSpeedModifier.Value;
        monster.agent.isStopped = false;
        monster.agent.SetDestination(destination.Value);
    }

    public override NodeResult Execute()
    {
        if(isDetect.Value) return NodeResult.success;
        if (monster.Sound.GroundChange) monster.Sound.PlayStepSound((EyeTypeMonsterState)curState.Value);
        if (monster.agent.pathPending) return NodeResult.running;
        if (monster.agent.hasPath) return NodeResult.running;
        if (monster.agent.remainingDistance < stopDistance) return NodeResult.success;
        return NodeResult.failure;
    }

    public override void OnExit()
    {
        //Debug.Log($"MonsterMoveToDestination - OnExit()");
        haveDestination.Value = false;
        isOriginPosition.Value = false;
    }
}
