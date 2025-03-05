using MBT;
using UnityEngine;
using UnityEngine.AI;

public class EyeTypeMonster : MonoBehaviour, IDeactivate
{
    [field: Header("References")]
    [field: SerializeField] private EyeTypeMonsterDataSO MonsterData { get; set; }
    public Blackboard bb;

    [field: Header("State")]
    public EyeTypeMonsterState curState;

    [field: Header("Animations")]
    public Animator animator;

    [field: Header("Nav")]
    public NavMeshAgent agent;

    [field: Header("Setting")]
    public EyeTypeMonsterSetting monsterSetting;
    [field: SerializeField] private Transform Destination { get; set; }
    [field: SerializeField] private Transform TargetNpc { get; set; }

    private void OnEnable()
    {
        GameManager.Instance.OnGameover += Deactivate;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameover -= Deactivate;
    }

    private void Start()
    {
        if (bb != null)
        {
            SetBlackboardVariable("findRange", MonsterData.Data.FindRange);
            SetBlackboardVariable("chaseRange", MonsterData.Data.ChaseRange);
            SetBlackboardVariable("attackRange", MonsterData.Data.AttackRange);
            SetBlackboardVariable("viewAngle", MonsterData.Data.ViewAngle);
            SetBlackboardVariable("baseSpeed", MonsterData.Data.BaseSpeed);
            SetBlackboardVariable("walkSpeedModifier", MonsterData.Data.WalkSpeedModifier);
            SetBlackboardVariable("runSpeedModifier", MonsterData.Data.RunSpeedModifier);

            if (HasSetting(EyeTypeMonsterSetting.CanPatrol)) SetBlackboardVariable("canPatrol", true);
            else
            {
                SetBlackboardVariable("originPosition", transform.position);
                SetBlackboardVariable("originDirection", transform.rotation);
            }

            // target -> destination -> work
            if (HasSetting(EyeTypeMonsterSetting.HaveTarget))
            {
                if (TargetNpc != null)
                {
                    SetBlackboardVariable("targetNpc", TargetNpc);
                    SetBlackboardVariable("haveTarget", true);
                }
            }

            if (HasSetting(EyeTypeMonsterSetting.HaveDestination))
            {
                if (Destination != null)
                {
                    SetBlackboardVariable("destination", Destination.position);
                    SetBlackboardVariable("haveDestination", true);
                    Destroy(Destination.gameObject);
                }
            }

            SetBlackboardVariable("isWork", HasSetting(EyeTypeMonsterSetting.IsWork));            
        }
    }

    public void MonsterWork(bool isWork)
    {
        SetBlackboardVariable("isWork", isWork);
    }

    private void SetBlackboardVariable<T>(string key, T value)
    {
        bb.GetVariable<Variable<T>>(key).Value = value;
    }

    private bool HasSetting(EyeTypeMonsterSetting setting) => (monsterSetting & setting) == setting;

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
