using MBT;
using UnityEditor.ShaderGraph;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Detect Player Service")]
public class DetectPlayerService : Service
{
    public TransformReference variableToSet;
    public TransformReference self;
    public TransformReference detectorHigh;
    public TransformReference detectorLow;
    public TransformReference player;
    public IntReference state;
    public Vector3Reference targetPos;

    public float findRange = 30f;
    public float chaseRange = 50f;
    public float viewAngle = 120f;
    public LayerMask obstructionMask;
    public LayerMask targetMask;

    private float _curFindRange;

    public override void Task()
    {
        // 플레이어 탐지시, 탐지 생략 후 추적
        if (state.Value == (int)MonsterState.MoveState) return;

        SetRange();
        //Debug.Log($"DetectPlayerService - Task() - {_curFindRange}");
        Detect();
    }

    private void Detect()
    {
        variableToSet.Value = null;
        if (!GetIsPlayerInFieldOfView() || !GetIsPlayerInFindRange()) return;
        //Debug.Log($"DetectPlayerService - Detect() - 탐지 시작 - {detectorLow.Value.position}");

        Vector3 directionToTargetLow = (new Vector3(player.Value.position.x, detectorLow.Value.position.y, player.Value.position.z) - detectorLow.Value.position).normalized;
        Vector3 directionToTargetHigh = (new Vector3(player.Value.position.x, detectorHigh.Value.position.y, player.Value.position.z) - detectorHigh.Value.position).normalized;
        
        if (Physics.Raycast(detectorLow.Value.position, directionToTargetLow, _curFindRange, targetMask))
        {
            //Debug.Log("DetectPlayerService - Detect() - low");
            variableToSet.Value = player.Value;
            targetPos.Value = variableToSet.Value.position;
            state.Value = (int)MonsterState.MoveState;
            return; 
        }
        else variableToSet.Value = null;

        if (Physics.Raycast(detectorHigh.Value.position, directionToTargetHigh, _curFindRange, targetMask))
        {
            //Debug.Log("DetectPlayerService - Detect() - high");
            variableToSet.Value = player.Value;
            targetPos.Value = variableToSet.Value.position;
            state.Value = (int)MonsterState.MoveState;
            return;
        }
        else variableToSet.Value = null;
    }

    private bool GetIsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = player.Value.position - self.Value.position;
        float angle = Vector3.Angle(self.Value.forward, directionToPlayer);
        return angle < viewAngle * 0.5f;
    }

    private bool GetIsPlayerInFindRange()
    {
        return Vector3.Distance(self.Value.position, player.Value.position) <= findRange;
    }

    private void SetRange()
    {
        _curFindRange = findRange;
        //if(state.Value == (int)MonsterState.MoveState) _curFindRange = chaseRange;
    }
}
