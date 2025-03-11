using MBT;
using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("")]
[MBTNode("Example/Monster Patrol")]
public class MonsterPatrol : Leaf
{
    public BoolReference skipValueIsDetect;//isDetect
    public FloatReference baseSpeed;
    public FloatReference walkSpeedModifier;
    public IntReference curState;
    public IntReference monsterType;
    public BoolReference canAttack;

    public Monster monster;
    public float stopDistance = 2f;
    private Vector3 _destination;

    public override void OnEnter()
    {
        MonsterState state = (MonsterState)curState.Value;
        if (state != MonsterState.Walk)
        {
            monster.Sound.StopStepAudio();
            curState.Value = (int)MonsterState.Walk;
        }
        monster.Sound.PlayStepSound((MonsterState)curState.Value);

        if (monsterType.Value == 1) monster.SetAnimation(false, true, false, false, false);
        else monster.SetAnimation(false, true, false, false);

        _destination = monster.GetPatrolPosition();
        monster.agent.speed = baseSpeed.Value * walkSpeedModifier.Value;
        monster.agent.isStopped = false;
        MoveToDestination(_destination);
    }

    public override NodeResult Execute()
    {
        if (skipValueIsDetect.Value || canAttack.Value) return NodeResult.failure;
        if (monster.Sound.GroundChange) monster.Sound.PlayStepSound((MonsterState)curState.Value);
        if (monster.agent.pathPending) return NodeResult.running;
        if (monster.agent.hasPath) return NodeResult.running;
        if (monster.agent.remainingDistance < stopDistance) return NodeResult.success;
        return NodeResult.failure;
    }

    private void MoveToDestination(Vector3 vector3)
    {
        if (NavMesh.SamplePosition(vector3, out NavMeshHit hit, Vector3.Distance(transform.position, vector3), NavMesh.AllAreas)) monster.agent.SetDestination(hit.position);
    }
}
