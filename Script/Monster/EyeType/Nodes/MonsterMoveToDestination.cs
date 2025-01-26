using MBT;
using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move To Destination")]
public class MonsterMoveToDestination : Leaf
{
    public IntReference curState;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;
    public BoolReference haveDestination;
    public BoolReference variableToSkip;
    public TransformReference destination;

    public Animator animator;
    public NavMeshAgent agent;
    public float stopDistance = 1f;


    public override void OnEnter()
    {
        //Debug.Log($"MonsterMoveToDestination - OnEnter()");

        curState.Value = (int)EyeTypeMonsterState.Move;
        
        animator.SetBool("Run", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);

        agent.speed = baseSpeed.Value * runSpeedModifier.Value;
        agent.isStopped = false;
        agent.SetDestination(destination.Value.position);

        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if(variableToSkip.Value) return NodeResult.success;

        // Check if path is ready
        if (agent.pathPending)
        {
            //Debug.Log("if (agent.pathPending)");
            if (agent.isStopped) agent.isStopped = false;
            return NodeResult.running;
        }
        // Check if agent is very close to destination
        if (agent.remainingDistance < stopDistance)
        {
            //Debug.Log("if (agent.remainingDistance < stopDistance)");
            return NodeResult.success;
        }
        // Check if there is any path (if not pending, it should be set)
        if (agent.hasPath)
        {
            //Debug.Log("if (agent.hasPath)");
            if (agent.isStopped) agent.isStopped = false;
            return NodeResult.running;
        }
        // By default return failure
        return NodeResult.failure;
    }

    public override void OnExit()
    {
        //Debug.Log($"MonsterMoveToDestination - OnExit()");

        base.OnExit();
        haveDestination.Value = false;
    }
}
