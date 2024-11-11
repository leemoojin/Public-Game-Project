using MBT;
using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("")]
[MBTNode("Example/Move To Vector")]
public class MoveToVector : Leaf
{
    public Vector3Reference destination;
    public NavMeshAgent agent;
    public float stopDistance = 2f;
    public float updateInterval = 1f;
    private float time = 0;

    public IntReference state;

    public override void OnEnter()
    {
        time = 0;
        agent.isStopped = false;
        agent.SetDestination(destination.Value);
    }

    public override NodeResult Execute()
    {
        time += Time.deltaTime;
        if (time > updateInterval)
        {
            time = 0;
            agent.SetDestination(destination.Value);
        }
        if (agent.pathPending)
        {
            return NodeResult.running;
        }
        if (agent.remainingDistance < stopDistance)
        {
            //Debug.Log("MoveNavmeshAgent - Execute() - 도착 성공");
            return NodeResult.success;
        }
        if (agent.hasPath)
        {
            return NodeResult.running;
        }
        //Debug.Log("MoveNavmeshAgent - Execute() - 도착 실패");
        return NodeResult.failure;
    }

    public override void OnExit()
    {
        agent.isStopped = true;
        // agent.ResetPath();

        //Debug.Log("MoveNavmeshAgent - OnExit()");
    }
}
