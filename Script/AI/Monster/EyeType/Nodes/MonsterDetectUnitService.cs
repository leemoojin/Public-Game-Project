using MBT;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Detect Unit Service")]
public class MonsterDetectUnitService : Service
{
    public BoolReference isDetect;
    public BoolReference skipValueIsWork;
    public BoolReference isLostTarget;

    public TransformReference self;
    public TransformReference target;   

    public FloatReference viewAngle;
    public FloatReference findRange;
    public FloatReference chaseRange;

    public IntReference curState;

    public LayerMask obstructionMask;
    public LayerMask targetMask;

    public Transform detectorHigh;
    public Transform detectorLow;
    private float _stepAngle;
    private int _rayCount;

    private List<Transform> _detectList = new List<Transform>();

    public override void OnEnter()
    {
        //Debug.Log($"MonsterDetectUnitService - OnEnter()");
        _rayCount = Mathf.CeilToInt(viewAngle.Value * 0.52f);
        _stepAngle = viewAngle.Value / _rayCount;
        base.OnEnter();        
    }

    public override void Task()
    {
        if (!skipValueIsWork.Value) return;
        DetectTargets();
    }

    private void DetectTargets()
    {
        _detectList.Clear();
        float range = curState.Value == (int)EarTypeMonsterState.Run ? chaseRange.Value : findRange.Value;
        bool foundTarget = false;

        for (int i = 0; i <= _rayCount; i++)
        {
            float angle = -viewAngle.Value / 2 + _stepAngle * i;
            Vector3 directionLow = Quaternion.Euler(0, angle, 0) * detectorLow.forward;
            Vector3 directionHigh = Quaternion.Euler(0, angle, 0) * detectorHigh.forward;
            Debug.DrawRay(detectorLow.position, directionLow * range, Color.green);

            if (PerformRaycast(detectorLow.position, directionLow, range) || PerformRaycast(detectorHigh.position, directionHigh, range))
            {
                foundTarget = true;
            }           
        }

        if (!foundTarget)
        {
            ResetTarget();
            //Debug.Log($"detect fail, targe : {target.Value}");
        }
        else if (isLostTarget.Value) isLostTarget.Value = false;

        AssignClosestTarget();
    }

    private bool PerformRaycast(Vector3 origin, Vector3 direction, float range)
    {
        if (Physics.Raycast(origin, direction, range, obstructionMask)) return false;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, range, targetMask))
        {
            if (hit.collider != null)
            {
                _detectList.Add(hit.collider.transform);
                //Debug.Log($"Detected {hit.collider.name}");
                return true;
            }
        }
        return false;
    }

    private void AssignClosestTarget()
    {
        if (_detectList.Count == 0)
        {
            ResetTarget();
            return;
        }

        Transform closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (var detected in _detectList)
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
            isDetect.Value = true;
            isLostTarget.Value = false;
            //Debug.Log($"Å½Áö´ë»ó : {target.Value}, Å½Áö¿©ºÎ : {variableToSet.Value}");
        }
        else
        {
            ResetTarget();
        }

        //Debug.Log($"Target {target.Value.name}");
    }

    private void ResetTarget()
    {
        isLostTarget.Value = true;
        target.Value = null;    
        isDetect.Value = false;   
    }
}
