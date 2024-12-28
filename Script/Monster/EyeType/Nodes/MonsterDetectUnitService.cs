using MBT;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[AddComponentMenu("")]
[MBTNode("Example/Monster Detect Unit Service")]
public class MonsterDetectUnitService : Service
{

    //public BoolReference variableToSet;// isUnitDetect
    public TransformReference self;
    public TransformReference detectorHigh;
    public TransformReference detectorLow;
    public TransformReference target;// Unit
    public FloatReference viewAngle;
    public FloatReference findRange;


    public LayerMask obstructionMask;
    public LayerMask targetMask;
    public int rayCount = 30;
    public List<Transform> unitList;

    //private bool _isLookObstruct = false;

    public override void OnEnter()
    {
        base.OnEnter();
        unitList = new List<Transform>();
    }

    public override void Task()
    {
        Detect();
    }

    private void Detect()
    {
        float range = findRange.Value;
        float stepAngle = viewAngle.Value / rayCount;
        unitList.Clear();

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = -viewAngle.Value / 2 + stepAngle * i;
            Vector3 directionLow = Quaternion.Euler(0, angle, 0) * detectorLow.Value.forward;
            Vector3 directionHigh = Quaternion.Euler(0, angle, 0) * detectorHigh.Value.forward;


            if (Physics.Raycast(detectorLow.Value.position, directionLow, out RaycastHit hitLow, range, targetMask) && !Physics.Raycast(detectorLow.Value.position, directionLow, range, obstructionMask))
            {
                Debug.Log($"DetectedLow {hitLow.collider.name} at angle {angle}");
                Debug.Log($"DetectedLow {hitLow.collider} at angle {angle}");

                // 眠啊 贸府 肺流
                //unitList.Add(hitLow.transform);
                CheckDetectList(hitLow.transform);


            }

            if (Physics.Raycast(detectorHigh.Value.position, directionHigh, out RaycastHit hitHigh, range, targetMask) && !Physics.Raycast(detectorHigh.Value.position, directionHigh, range, obstructionMask))
            {
                //Debug.Log($"DetectedHigh {hitHigh.collider.name} at angle {angle}");
                // 眠啊 贸府 肺流
                //unitList.Add(hitLow.transform);
                CheckDetectList(hitLow.transform);

            }
        }


    }

    private void CheckDetectList(Transform transform)
    {
        if (unitList.Count == 0)
        {
            unitList.Add(transform);
            return;
        }

        bool isSame = false;

        for (int i = 0; i < unitList.Count; i++)
        {
            if (unitList[i] == transform)
            {
                isSame = true;
            }
        }

        if (!isSame)
        {
            unitList.Add(transform);
        }

    }

}
