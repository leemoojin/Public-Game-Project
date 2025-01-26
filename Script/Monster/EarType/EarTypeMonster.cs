using MBT;
using UnityEngine;

public class EarTypeMonster : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public MonsterDataSO MonsterData { get; private set; }
    public Blackboard bb;

    [field: Header("State")]
    public EarTypeMonsterState curState;

    [field: Header("Animations")]
    public Animator animator;

    [field: Header("Setting")]
    public MonsterSetting monsterSetting;

    private void Start()
    {
        if (bb != null)
        {
            bb.GetVariable<Variable<float>>("detectRange").Value = MonsterData.EarType.DetectRange;
            bb.GetVariable<Variable<float>>("detectNoiseMin").Value = MonsterData.EarType.DetectNoiseMin;
            bb.GetVariable<Variable<float>>("detectNoiseMax").Value = MonsterData.EarType.DetectNoiseMax;
            bb.GetVariable<Variable<float>>("baseSpeed").Value = MonsterData.EarType.BaseSpeed;
            bb.GetVariable<Variable<float>>("walkSpeedModifier").Value = MonsterData.EarType.WalkSpeedModifier;
            bb.GetVariable<Variable<float>>("runSpeedModifier").Value = MonsterData.EarType.RunSpeedModifier;
            bb.GetVariable<Variable<float>>("attackRange").Value = MonsterData.EarType.AttackRange;
            bb.GetVariable<Variable<int>>("curState").Value = (int)EarTypeMonsterState.Idle;

            if ((monsterSetting & MonsterSetting.CanPatrol) == MonsterSetting.CanPatrol) bb.GetVariable<Variable<bool>>("canPatrol").Value = true;
            if ((monsterSetting & MonsterSetting.IsWork) == MonsterSetting.IsWork) bb.GetVariable<Variable<bool>>("isWork").Value = true;
        }
        animator.SetBool("Idle", true);
    }
}
