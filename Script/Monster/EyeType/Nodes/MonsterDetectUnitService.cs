using MBT;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[AddComponentMenu("")]
[MBTNode("Example/Monster Detect Unit Service")]
public class MonsterDetectUnitService : Service
{

    public BoolReference variableToSet;// isUnitDetect
    public TransformReference self;
    public TransformReference detectorHigh;
    public TransformReference detectorLow;
    public TransformReference target;// Unit
    public FloatReference viewAngle;
    public FloatReference findRange;


    public LayerMask obstructionMask;
    public LayerMask targetMask;
    public int rayCount = 30;
    public List<Transform> detectList;

    //private bool _isLookObstruct = false;

    public override void OnEnter()
    {
        base.OnEnter();
        detectList = new List<Transform>();
    }

    public override void Task()
    {
        Detect();
    }

    private void Detect()
    {
        float range = findRange.Value;
        float stepAngle = viewAngle.Value / rayCount;

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = -viewAngle.Value / 2 + stepAngle * i;
            Vector3 directionLow = Quaternion.Euler(0, angle, 0) * detectorLow.Value.forward;
            Vector3 directionHigh = Quaternion.Euler(0, angle, 0) * detectorHigh.Value.forward;

            Debug.DrawRay(detectorLow.Value.position, directionLow * range, Color.green);

            if (Physics.Raycast(detectorLow.Value.position, directionLow, out RaycastHit hitLow, range, targetMask) && !Physics.Raycast(detectorLow.Value.position, directionLow, range, obstructionMask))
            {
                //Debug.Log($"DetectedLow {hitLow.collider.name} at angle {angle}");        
                CheckDetectList(hitLow.collider.transform);
            }
            else if (Physics.Raycast(detectorHigh.Value.position, directionHigh, out RaycastHit hitHigh, range, targetMask) && !Physics.Raycast(detectorHigh.Value.position, directionHigh, range, obstructionMask))
            {
                //Debug.Log($"DetectedHigh {hitHigh.collider.name} at angle {angle}");
                CheckDetectList(hitHigh.collider.transform);
            }
        }

        CheckDistance();
    }

    private void CheckDetectList(Transform transform)
    {
        if (detectList.Count == 0)
        {
            detectList.Add(transform);
            //Debug.Log($"추가 {transform}");
            return;
        }

        bool isSame = false;
        for (int i = 0; i < detectList.Count; i++)
        {
            //Debug.Log($"비교 {detectList[i]}, {transform}");

            if (detectList[i] == transform)
            {
                isSame = true;
            }
        }

        if (!isSame)
        {
            detectList.Add(transform);
            //Debug.Log($"비교 추가 {transform}");
        }
    }

    private void CheckDistance()
    {
        if (detectList.Count == 0)
        {
            target.Value = null;
            variableToSet.Value = false;
            detectList.Clear();
            //Debug.Log("발견 못함");
            return;
        }
        
        Transform temp = null;
        for (int i = 0; i < detectList.Count; i++)
        {
            if (i == 0)
            {
                temp = detectList[i];
                continue;
            }
            if (Vector3.Distance(self.Value.position, temp.position) > Vector3.Distance(self.Value.position, detectList[i].position)) temp = detectList[i]; 
        }

        target.Value = temp;
        variableToSet.Value = true;
        //Debug.Log("발견");
    }

}
