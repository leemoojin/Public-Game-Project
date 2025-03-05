using MBT;
using System.Collections;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Patrol Stand")]
public class MonsterPatrolStand : MoveToVector
{
    public BoolReference variableToSkip;//isDetect
    public FloatReference baseSpeed;
    public FloatReference walkSpeedModifier;
    public IntReference curState;
    public IntReference monsterType;
    public BoolReference canAttack;
    public TransformReference self;

    public Animator animator;
    public UnitSoundSystem soundSystem;
    public EyeTypeMonster_Delete eyeTypeMonster;
    //public float moveSpeed = 5f;
    public bool isStand;
    private Quaternion _originRotation;
    private bool _isChange;

    public override void OnEnter()
    {
        //Debug.Log("MonsterPatrol - OnEnter()");
        if (isStand)
        {
            _isChange = false;
        }

        //state.Value = (int)MonsterState.PatrolState;
        agent.speed = baseSpeed.Value * walkSpeedModifier.Value;

        if (monsterType.Value == 1)
        {           
            //Debug.Log($"MonsterPatrol - OnEnter() - curState walk : {(EyeTypeMonsterState)curState.Value}");
            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", true);
            if (isStand && Vector3.Distance(self.Value.position, destination.Value) < stopDistance)
            {
                soundSystem.StopStepAudio();
                curState.Value = (int)EyeTypeMonsterState.Idle;
                animator.SetBool("Idle", true);
                animator.SetBool("Walk", false);
                return;
            }

            EyeTypeMonsterState state = (EyeTypeMonsterState)curState.Value;
            if (!state.HasFlag(EyeTypeMonsterState.Walk))
            {
                soundSystem.StopStepAudio();
                curState.Value = (int)EyeTypeMonsterState.Walk;
            }
            soundSystem.PlayStepSound((EyeTypeMonsterState)curState.Value);

        }
        //else if (monsterType.Value == 2)
        //{
        //    curState.Value = (int)EarTypeMonsterState.Walk;
        //    animator.SetBool("Idle", false);
        //    animator.SetBool("Walk", true);
        //    animator.SetBool("Run", false);
        //    animator.SetBool("Focus", false);
        //    //animator.SetBool("Attack", false);
        //    if (isStand && Vector3.Distance(self.Value.position, destination.Value) < stopDistance)
        //    {
        //        animator.SetBool("Idle", true);
        //        animator.SetBool("Walk", false);
        //    }
        //}
        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (variableToSkip.Value || canAttack.Value)
        {
            //Debug.Log("MonsterPatrol - variableToSkip()");
            return NodeResult.failure;
        }

        if (soundSystem.GroundChange)
        {
            soundSystem.PlayStepSound((EyeTypeMonsterState)curState.Value);
        }

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
            if (isStand)
            {
                if (!_isChange) StartCoroutine(MonsterRotation());
                else return NodeResult.success;
            }
            else return NodeResult.success;

            //Debug.Log("MoveToVector - Execute() - 도착 성공");
        }
        if (agent.hasPath)
        {
            return NodeResult.running;
        }

        //Debug.Log("MoveToVector - Execute() - 이동 중");
        return NodeResult.running;
    }

    IEnumerator MonsterRotation()
    {
        Quaternion startRotation = self.Value.rotation;
        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            //self.Value.rotation = Quaternion.Slerp(startRotation, eyeTypeMonster.OriginRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //self.Value.rotation = eyeTypeMonster.OriginRotation;
        _isChange = true;
    }
}

