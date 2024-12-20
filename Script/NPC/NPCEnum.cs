using System;
using UnityEngine;

namespace NPC
{
    [Flags]
    public enum NPCState
    {
        Wait,
        Idle,
        Move,
        Run,
        Crouch,
        Dead
    }
}

