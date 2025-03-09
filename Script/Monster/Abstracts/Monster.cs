using MBT;
using UnityEngine;
using UnityEngine.AI;

public abstract class Monster : MonoBehaviour, IDeactivate
{
    [field: Header("References")]
    [field: SerializeField] protected Blackboard BB { get; set; }
    [field: SerializeField] public GameObject PatrolPosition { get; set; }

    [field: Header("Animations")]
    public Animator animator;

    [field: Header("Nav")]
    public NavMeshAgent agent;

    [field: Header("State")]
    public EyeTypeMonsterState curState;

    [field: Header("Setting")]
    public MonsterSetting monsterSetting;
    [field: SerializeField] protected Transform Destination { get; set; }
    [field: SerializeField] protected Transform TargetNpc { get; set; }

    private Patrol Patrol { get; set; }

    private void Awake()
    {
        Patrol = new Patrol(this);
        if (PatrolPosition != null) Patrol.PatrolTransforms = PatrolPosition.GetComponentsInChildren<Transform>();
        Patrol.SetPatrolPositions(HasSetting(MonsterSetting.KeepPosition));
    }

    protected virtual void Start()
    {
        if (HasSetting(MonsterSetting.KeepPosition))
        {
            Destroy(PatrolPosition.gameObject);
            PatrolPosition = null;
        }
    }

    public Vector3 GetPatrolPosition()
    {
        return Patrol.GetPatrolPosition(HasSetting(MonsterSetting.KeepPosition));
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameover += Deactivate;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameover -= Deactivate;
    }

    public void MonsterWork(bool isWork)
    {
        SetBlackboardVariable("isWork", isWork);
    }

    protected void SetBlackboardVariable<T>(string key, T value)
    {
        BB.GetVariable<Variable<T>>(key).Value = value;
    }

    protected bool HasSetting(MonsterSetting setting) => (monsterSetting & setting) == setting;

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
