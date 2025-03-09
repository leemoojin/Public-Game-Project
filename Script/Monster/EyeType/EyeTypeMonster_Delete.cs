using MBT;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EyeTypeMonster_Delete : MonoBehaviour, IDeactivate
{
    [field: Header("References")]
    [field: SerializeField] public MonsterDataSO MonsterData { get; private set; }
    public Blackboard bb;
    public Transform detectorHigh;
    public Transform detectorLow;

    [field: Header("State")]
    public EyeTypeMonsterState curState;

    [field: Header("Animations")]
    public Animator animator;

    [field: Header("Destination")]
    public Transform destination;
    public Transform target;
    public NavMeshAgent agent;
    //private bool _isArrival;

    [field: Header("Setting")]
    public MonsterSetting_ monsterSetting;

    protected virtual void Start()
    {
        if (bb != null)
        {
            bb.GetVariable<Variable<float>>("findRange").Value = MonsterData.EyeType.FindRange;
            bb.GetVariable<Variable<float>>("moveRange").Value = MonsterData.EyeType.ChaseRange;
            bb.GetVariable<Variable<float>>("attackRange").Value = MonsterData.EyeType.AttackRange;
            bb.GetVariable<Variable<float>>("viewAngle").Value = MonsterData.EyeType.ViewAngle;
            bb.GetVariable<Variable<float>>("baseSpeed").Value = MonsterData.EyeType.BaseSpeed;
            bb.GetVariable<Variable<float>>("walkSpeedModifier").Value = MonsterData.EyeType.WalkSpeedModifier;
            bb.GetVariable<Variable<float>>("runSpeedModifier").Value = MonsterData.EyeType.RunSpeedModifier;

            if ((monsterSetting & MonsterSetting_.HaveDestination) == MonsterSetting_.HaveDestination)
            {
                //Debug.Log($"목적지 이동");
                // move to destination
                bb.GetVariable<Variable<bool>>("haveDestination").Value = true;
            }
            else 
            {
                bb.GetVariable<Variable<int>>("curState").Value = (int)EyeTypeMonsterState.Idle;
                animator.SetBool("Idle", true);
            }

            if ((monsterSetting & MonsterSetting_.HaveTarget) == MonsterSetting_.HaveTarget)
            {
                if (target != null) bb.GetVariable<Variable<Transform>>("targetDestination").Value = target;
                bb.GetVariable<Variable<bool>>("haveDestination").Value = true;
            }
            else
            {
                bb.GetVariable<Variable<int>>("curState").Value = (int)EyeTypeMonsterState.Idle;
                animator.SetBool("Idle", true);
            }

            if ((monsterSetting & MonsterSetting_.CanPatrol) == MonsterSetting_.CanPatrol) bb.GetVariable<Variable<bool>>("canPatrol").Value = true;
            if ((monsterSetting & MonsterSetting_.IsWork) == MonsterSetting_.IsWork) bb.GetVariable<Variable<bool>>("isWork").Value = true;
        }

        //OriginRotation = transform.rotation;
        GameManager.Instance.OnGameover += Deactivate;
    }

    public void MonsterWork(bool isWork)
    {
        bb.GetVariable<Variable<bool>>("isWork").Value = isWork;
    }

    public void Deactivate()
    {
        if (this == null) return;
        gameObject.SetActive(false);    
    }
}
