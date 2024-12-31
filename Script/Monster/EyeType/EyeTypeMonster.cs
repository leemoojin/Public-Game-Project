using MBT;
using NPC;
using UnityEngine;

public class EyeTypeMonster : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public MonsterDataSO MonsterData { get; private set; }
    public Blackboard BB;

    [field: Header("State")]
    public EyeTypeMonsterState curState;

    [field: Header("Animations")]
    public Animator animator;

    [field: Header("Info")]
    public bool isWork;

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
            BB.GetVariable<Variable<bool>>("isWork").Value = isWork;
            BB.GetVariable<Variable<int>>("curState").Value = (int)EyeTypeMonsterState.Idle;
        }

        animator.SetBool("Idle", true);

        //curState = EyeTypeMonsterState.Idle;
        //Debug.Log($"idle : {curState}");
        //curState |= EyeTypeMonsterState.Walk;
        //Debug.Log($"idle, Walk : {curState}");
        //curState = curState | EyeTypeMonsterState.Move | EyeTypeMonsterState.Run;
        //Debug.Log($"idle, Walk, Move, Run : {curState}");

        //if ((curState & EyeTypeMonsterState.Run) == EyeTypeMonsterState.Run)
        //{
        //    Debug.Log($"include Run");
        //}

        //curState = curState & ~EyeTypeMonsterState.Idle & ~EyeTypeMonsterState.Walk;
        //Debug.Log($"Move, Run : {curState}");
        //int num = (int)curState;
        //Debug.Log($"Move, Run int num : {num}");
        //curState = (EyeTypeMonsterState)num;
        //Debug.Log($"Move, Run : {curState}");
        //curState = (EyeTypeMonsterState)num & ~EyeTypeMonsterState.Move;
        //Debug.Log($"Run : {curState}");
    }
}
