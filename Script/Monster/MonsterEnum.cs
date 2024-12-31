using System;

public enum EarTypeMonsterState
{
    IdleState,
    PatrolState,
    MoveState,
    FocusAround
}

[Flags]
public enum EyeTypeMonsterState
{
    Idle = 1 << 0,
    Move = 1 << 1,
    Walk = 1 << 2,
    Run = 1 << 3,
    Attack = 1 << 4    
}

[Flags]
public enum MonsterType
{
    EyeType = 1,
    EarType = 2
}