using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Player Target")]
public class MonsterMovePlayerTarget : MoveToTransform
{
    public TransformReference self;
    public IntReference curState;
    public IntReference monsterType;
    public FloatReference distanceToTarget;
    public FloatReference chaseRange;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;
    public FloatReference attackRange;
    public BoolReference skipValueLostToTarget;
    public BoolReference isOriginPosition;
    public BoolReference isLost;

    public Monster monster;
    private bool _canAttack;

    public override void OnEnter()
    {
        //Debug.Log($"MonsterMovePlayerTarget - OnEnter()");
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
        if (distanceToTarget.Value <= attackRange.Value)
        {
            _canAttack = true;
            return NodeResult.success;
        }

        if (skipValueLostToTarget.Value && distanceToTarget.Value >= chaseRange.Value) return NodeResult.failure;

        if (monster.Sound.GroundChange) monster.Sound.PlayStepSound((MonsterState)curState.Value);

        self.Value.LookAt(destination.Value);

        return base.Execute();
    }

    public override void OnExit()
    {
        base.OnExit();
        isOriginPosition.Value = false;
        if (!_canAttack) isLost.Value = true;
        //Debug.Log($"MonsterMovePlayerTarget - OnExit()");
    }
}
