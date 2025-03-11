using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move To Target")]
public class MonsterMoveToTarget : MoveToTransform
{
    public IntReference curState;
    public IntReference monsterType;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;
    public BoolReference isDetect;
    public BoolReference haveTarget;
    public BoolReference isOriginPosition;

    public Monster monster;

    public override void OnEnter()
    {
        MonsterState state = (MonsterState)curState.Value;
        if (!state.HasFlag(MonsterState.Run))
        {
            monster.Sound.StopStepAudio();
            curState.Value = (int)MonsterState.Run;
        }
        monster.Sound.PlayStepSound((MonsterState)curState.Value);
        agent.speed = baseSpeed.Value * runSpeedModifier.Value;
        monster.SetAnimation(false, false, true, false, false);
        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (isDetect.Value) return NodeResult.success;
        if (monster.Sound.GroundChange) monster.Sound.PlayStepSound((MonsterState)curState.Value);
        return base.Execute();
    }

    public override void OnExit()
    {
        //Debug.Log($"MonsterMoveToDestination - OnExit()");
        base.OnExit();
        haveTarget.Value = false;
        isOriginPosition.Value = false;
    }
}
