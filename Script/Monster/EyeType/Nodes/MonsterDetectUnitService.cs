using MBT;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[AddComponentMenu("")]
[MBTNode("Example/Monster Detect Unit Service")]
public class MonsterDetectUnitService : Service
{
    public BoolReference variableToSet;// isDetect
    public BoolReference variableToSkip;
    public BoolReference isLostTarget;
    public TransformReference self;
    public TransformReference detectorHigh;
    public TransformReference detectorLow;
    public TransformReference target;// Unit
    public FloatReference viewAngle;
    public FloatReference findRange;
    public FloatReference moveRange;
    public IntReference curState;

    public LayerMask obstructionMask;
    public LayerMask targetMask;
    public int rayCount = 30;
    //public List<Transform> detectList;

    private HashSet<Transform> detectList;

    public override void OnEnter()
    {
        base.OnEnter();
        //Debug.Log($"MonsterDetectUnitService - OnEnter()");
        detectList = new HashSet<Transform>();
        //detectList = new List<Transform>();
    }

    public override void Task()
    {
        if (!variableToSkip.Value) return;
        Detect();
    }

    private void Detect()
    {
        float range = curState.Value == (int)EarTypeMonsterState.Run ? moveRange.Value : findRange.Value;
        float stepAngle = viewAngle.Value / rayCount;
        bool isDetect = false;

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = -viewAngle.Value / 2 + stepAngle * i;
            Vector3 directionLow = Quaternion.Euler(0, angle, 0) * detectorLow.Value.forward;
            Vector3 directionHigh = Quaternion.Euler(0, angle, 0) * detectorHigh.Value.forward;
            Debug.DrawRay(detectorLow.Value.position, directionLow * range, Color.green);

            if (RaycastCheck(detectorLow.Value.position, directionLow, range) || RaycastCheck(detectorHigh.Value.position, directionHigh, range))
            {
                isDetect = true;
            }           
        }

        if (!isDetect)
        {            
            isLostTarget.Value = true;
            target.Value = null;
            variableToSet.Value = false;
            //Debug.Log("detect fail");
        }
        else if (isLostTarget.Value)
        {
            isLostTarget.Value = false;
        }

        CheckDistance();
    }

    private bool RaycastCheck(Vector3 origin, Vector3 direction, float range)
    {
        if (Physics.Raycast(origin, direction, range, obstructionMask))
        {
            return false;
        }

        if (Physics.Raycast(origin, direction, out RaycastHit hit, range, targetMask))
        {
            detectList.Add(hit.collider.transform);
            //Debug.Log($"Detected {hit.collider.name}");
            return true;
        }
        return false;
    }
    private void CheckDistance()
    {
        if (detectList == null || detectList.Count == 0)
        {          
            if (target.Value == null) variableToSet.Value = false;
            return;
        }

        Transform closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (var detected in detectList)
        {
            if (detected == null) continue;

            float distance = Vector3.Distance(self.Value.position, detected.position);
            if (distance < closestDistance)
            {
                closestTarget = detected;
                closestDistance = distance;
            }
        }

        if (closestTarget != null)
        {
            target.Value = closestTarget;
            variableToSet.Value = true;
            //Debug.Log($"Å½Áö´ë»ó : {target.Value}, Å½Áö¿©ºÎ : {variableToSet.Value}");
        }
        else
        {
            target.Value = null;
            variableToSet.Value = false;
        }

        //Debug.Log($"Target {target.Value.name}");
    }
}
