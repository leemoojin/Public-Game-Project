using MBT;
using UnityEngine;


[AddComponentMenu("")]
[MBTNode("Example/Set Monster Data")]
public class SetMonsterData : Service
{
    public override void OnAllowInterrupt()
    {
        Debug.Log("SetMonsterData - OnAllowInterrupt()")
;
        base.OnAllowInterrupt();

        // if null monsterdata set
    }

    public override void Task()
    {
       
    }

    public override void OnExit()
    {
        Debug.Log("SetMonsterData - OnExit()");

        base.OnExit();
    }
}
