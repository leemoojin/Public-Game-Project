using MBT;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EyeTypeMonster : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public MonsterDataSO MonsterData { get; private set; }
    public Blackboard BB;

    [field: Header("State")]
    public EyeTypeMonsterState curState;

    [field: Header("Animations")]
    public Animator animator;

    [field: Header("Destination")]
    public Transform destination;
    public NavMeshAgent agent;
    private bool _isArrival;

    [field: Header("Setting")]
    public MonsterSetting monsterSetting;
    //public bool isWork;

    private void Start()
    {
        if (BB != null)
        {
            BB.GetVariable<Variable<float>>("findRange").Value = MonsterData.EyeType.FindRange;
            BB.GetVariable<Variable<float>>("moveRange").Value = MonsterData.EyeType.ChaseRange;
            BB.GetVariable<Variable<float>>("viewAngle").Value = MonsterData.EyeType.ViewAngle;
            BB.GetVariable<Variable<float>>("baseSpeed").Value = MonsterData.EyeType.BaseSpeed;
            BB.GetVariable<Variable<float>>("walkSpeedModifier").Value = MonsterData.EyeType.WalkSpeedModifier;
            BB.GetVariable<Variable<float>>("runSpeedModifier").Value = MonsterData.EyeType.RunSpeedModifier;

            if ((monsterSetting & MonsterSetting.HaveDestination) == MonsterSetting.HaveDestination)
            {
                Debug.Log($"목적지 이동");
                // move to destination
                BB.GetVariable<Variable<bool>>("haveDestination").Value = true;
                //BB.GetVariable<Variable<bool>>("isWork").Value = isWork;
            }
            else 
            {
                //BB.GetVariable<Variable<bool>>("isWork").Value = isWork;
                BB.GetVariable<Variable<int>>("curState").Value = (int)EyeTypeMonsterState.Idle;
                animator.SetBool("Idle", true);
            }

            if ((monsterSetting & MonsterSetting.CanPatrol) == MonsterSetting.CanPatrol) BB.GetVariable<Variable<bool>>("canPatrol").Value = true;
            if ((monsterSetting & MonsterSetting.IsWork) == MonsterSetting.IsWork) BB.GetVariable<Variable<bool>>("isWork").Value = true;

        }

    }

    public void MonsterWork()
    {
        BB.GetVariable<Variable<bool>>("isWork").Value = true;
    }
}
