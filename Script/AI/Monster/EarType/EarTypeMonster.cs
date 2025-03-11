using UnityEngine;

public class EarTypeMonster : Monster
{
    [field: Header("SO")]
    [field: SerializeField] private EarTypeMonsterDataSO MonsterData { get; set; }

    protected override void Start()
    {
        base.Start();
        Initialization();
    }

    private void Initialization()
    {
        if (BB != null)
        {
            SetVariable("detectRangeMax", MonsterData.Data.DetectRangeMax);
            SetVariable("detectRangeMin", MonsterData.Data.DetectRangeMin);
            SetVariable("detectNoiseMax", MonsterData.Data.DetectNoiseMax);
            SetVariable("detectNoiseMin", MonsterData.Data.DetectNoiseMin);
            SetVariable("attackRange", MonsterData.Data.AttackRange);
            SetVariable("baseSpeed", MonsterData.Data.BaseSpeed);
            SetVariable("walkSpeedModifier", MonsterData.Data.WalkSpeedModifier);
            SetVariable("runSpeedModifier", MonsterData.Data.RunSpeedModifier);
            SetVariable("player", GameManager.Instance.Player.transform);
            if (TargetNpc != null) SetVariable("npc", TargetNpc);
            SetVariable("canPatrol", true);
            SetVariable("isWork", HasSetting(MonsterSetting.IsWork));
        }
    }
}
