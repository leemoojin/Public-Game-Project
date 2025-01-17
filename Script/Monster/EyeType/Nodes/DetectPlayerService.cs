using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Detect Player Service")]
public class DetectPlayerService : Service
{
    public BoolReference variableToSet;// isPlayerDetect

    public TransformReference self;
    public TransformReference detectorHigh;
    public TransformReference detectorLow;
    public TransformReference player;// 플레이어 데이터 접근 개선 필요
    public IntReference state;
    public TransformReference target;// Player

    // SO
    public float findRange = 30f;// SO
    //public float chaseRange = 50f;// SO
    public float viewAngle = 120f;// SO
    public LayerMask obstructionMask;
    public LayerMask targetMask;

    private float _curFindRange;
    private bool _isLookObstruct = false;

    public override void Task()
    {
        // 플레이어 탐지시, 탐지 생략 후 추적
        if (state.Value == (int)EarTypeMonsterState_.MoveState) return;

        SetRange();
        //Debug.Log($"DetectPlayerService - Task() - {_curFindRange}");
        Detect();
    }

    private void Detect()
    {
        _isLookObstruct = false;
        //variableToSet.Value = null;// 이전 코드

        if (!GetIsPlayerInFieldOfView() || !GetIsPlayerInFindRange()) return;
        //Debug.Log($"DetectPlayerService - Detect() - 탐지 시작 - {detectorLow.Value.position}");

        Vector3 directionToTargetLow = (new Vector3(player.Value.position.x, detectorLow.Value.position.y, player.Value.position.z) - detectorLow.Value.position).normalized;
        Vector3 directionToTargetHigh = (new Vector3(player.Value.position.x, detectorHigh.Value.position.y, player.Value.position.z) - detectorHigh.Value.position).normalized;

        RaycastHit hit;

        if (Physics.Raycast(detectorLow.Value.position, directionToTargetLow, out hit, _curFindRange, targetMask))
        {
            //Debug.Log("DetectPlayerService - Detect() - low");
            variableToSet.Value = true;
            target.Value = hit.transform;
            //state.Value = (int)MonsterState.MoveState;
            return;
        }
        else
        {
            _isLookObstruct = true;
            //variableToSet.Value = false;
        }

        if (Physics.Raycast(detectorHigh.Value.position, directionToTargetHigh, out hit, _curFindRange, targetMask))
        {
            //Debug.Log("DetectPlayerService - Detect() - high");
            variableToSet.Value = true;
            target.Value = hit.transform;
            //state.Value = (int)MonsterState.MoveState;
            return;
        }
        else
        {
            _isLookObstruct = true;
            //variableToSet.Value = false;
        }

        if (_isLookObstruct) variableToSet.Value = false;
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
