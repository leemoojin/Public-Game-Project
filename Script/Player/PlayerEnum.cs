using System;
using UnityEngine;

public class PlayerEnum
{
    [Flags]
    public enum PlayerState
    {
        IdleState,
        WalkState,
        RunState,
        CrouchState
    }

    public enum Grounds
    {
        Untagged,
        Concrete,
        Wet
    }
}
