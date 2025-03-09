using UnityEngine;

public class EyeTypeMonster : Monster, IDeactivate
{
    [field: Header("SO")]
    [field: SerializeField] private EyeTypeMonsterDataSO MonsterData { get; set; }

    protected override void Start()
    {
        base.Start();
        Initialization();
    }

    private void Initialization()
    {
        if (BB != null)
        {
            SetBlackboardVariable("findRange", MonsterData.Data.FindRange);
            SetBlackboardVariable("chaseRange", MonsterData.Data.ChaseRange);
            SetBlackboardVariable("attackRange", MonsterData.Data.AttackRange);
            SetBlackboardVariable("viewAngle", MonsterData.Data.ViewAngle);
            SetBlackboardVariable("baseSpeed", MonsterData.Data.BaseSpeed);
            SetBlackboardVariable("walkSpeedModifier", MonsterData.Data.WalkSpeedModifier);
            SetBlackboardVariable("runSpeedModifier", MonsterData.Data.RunSpeedModifier);

            if (HasSetting(MonsterSetting.CanPatrol)) SetBlackboardVariable("canPatrol", true);
            else
            {
                SetBlackboardVariable("originPosition", transform.position);
                SetBlackboardVariable("originDirection", transform.rotation);
            }

            // target -> destination -> work
            if (HasSetting(MonsterSetting.HaveTarget))
            {
                if (TargetNpc != null)
                {
                    SetBlackboardVariable("targetNpc", TargetNpc);
                    SetBlackboardVariable("haveTarget", true);
                }
            }

            if (HasSetting(MonsterSetting.HaveDestination))
            {
                if (Destination != null)
                {
                    SetBlackboardVariable("destination", Destination.position);
                    SetBlackboardVariable("haveDestination", true);
                    Destroy(Destination.gameObject);
                }
            }

            SetBlackboardVariable("isWork", HasSetting(MonsterSetting.IsWork));
        }
    }
}
