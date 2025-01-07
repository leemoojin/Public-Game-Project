using MBT;
using UnityEngine;

public class EarTypeMonster : MonoBehaviour
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
            BB.GetVariable<Variable<float>>("detectRange").Value = MonsterData.EarType.DetectRange;
            BB.GetVariable<Variable<float>>("detectNoiseMin").Value = MonsterData.EarType.DetectNoiseMin;
            BB.GetVariable<Variable<float>>("baseSpeed").Value = MonsterData.EarType.BaseSpeed;
            BB.GetVariable<Variable<float>>("walkSpeedModifier").Value = MonsterData.EarType.WalkSpeedModifier;
            BB.GetVariable<Variable<float>>("runSpeedModifier").Value = MonsterData.EarType.RunSpeedModifier;
            BB.GetVariable<Variable<float>>("attackRange").Value = MonsterData.EarType.AttackRange;

            BB.GetVariable<Variable<bool>>("isWork").Value = isWork;
            BB.GetVariable<Variable<int>>("curState").Value = (int)EarTypeMonsterState.Idle;
        }

        animator.SetBool("Idle", true);
    }
}
