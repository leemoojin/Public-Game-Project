using UnityEngine;
using MBT;

public class EyeTypeMonsterStand : EyeTypeMonster_Delete
{
    // EyeTypeMonsterStand Setting = isWork

    protected override void Start()
    {
        if (bb != null)
        {
            bb.GetVariable<Variable<Vector3>>("standPosition").Value = transform.position;
            bb.GetVariable<Variable<Quaternion>>("standRotate").Value = transform.rotation;

        }
        base.Start();
    }
}
