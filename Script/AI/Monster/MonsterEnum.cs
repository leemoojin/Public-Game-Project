using System;

[Flags]
public enum MonsterState
{
    Idle = 1,
    Walk = 2,
    Run = 3,
    Attack = 4,
    Lost = 5,
}

[Flags]
public enum MonsterType
{
    EyeType = 1,
    EarType = 2
}

[Flags]
public enum MonsterSetting
{
    IsWork = 1 << 0,
    CanPatrol = 1 << 1,
    KeepPosition = 1 << 2,
    HaveDestination = 1 << 3,
    HaveTarget = 1 << 4,
}