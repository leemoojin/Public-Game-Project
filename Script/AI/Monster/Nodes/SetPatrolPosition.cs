using MBT;
using UnityEngine;


[MBTNode("Example/Set Patrol Position")]
[AddComponentMenu("")]
public class SetPatrolPosition : Leaf
{
    public Vector3Reference variableToSetPatrolPos;
    public Vector3Reference variableToSetOriginPos;
    public TransformReference self;
    public BoolReference variableToSkip;
    //public IntReference curState;
    public bool isStartOriginPos;
    public float patrolRangeMin = 30f;
    public float patrolRangeMax = 50f;
    public int repeatNum = 50;

    private Vector3 _centerPos;
    private Vector3 _newPos;
    private Vector3 _tempPos;

    public override void OnEnter()
    {
        //Debug.Log("SetPatrolPosition - OnEnter()");

        base.OnEnter();

        if(isStartOriginPos && variableToSetOriginPos.Value == Vector3.zero) variableToSetOriginPos.Value = self.Value.position;
        //if (variableToSetOriginPos.Value == Vector3.zero) variableToSetOriginPos.Value = self.Value.position;        
    }

    public override NodeResult Execute()
    {
        if (variableToSkip.Value)
        {
            //Debug.Log($"SetPatrolPosition - Execute() - variableToSkip");
            return NodeResult.failure;
        }

        GetPosition();
        if (_newPos != Vector3.zero)
        {
            variableToSetPatrolPos.Value = _newPos;
            //Debug.Log($"SetPatrolPosition - Execute() - 이동거리 : {Vector3.Distance(_centerPos, _newPos)}");
            return NodeResult.success;
        }
        //Debug.Log($"SetPatrolPosition - Execute() - running");
        return NodeResult.running;
    }

    private void GetPosition()
    {
        if (isStartOriginPos) _centerPos = variableToSetOriginPos.Value;
        else _centerPos = self.Value.position;

        _newPos = Vector3.zero;
        _tempPos = Vector3.zero;

        for (int i = 0; i < repeatNum; i++)
        {
            _tempPos = Random.insideUnitSphere * patrolRangeMax;
            _tempPos.y = 0;
            _tempPos += _centerPos;

            if(_newPos == Vector3.zero) _newPos = _tempPos;
            if (Vector3.Distance(_centerPos, _newPos) < Vector3.Distance(_centerPos, _tempPos)) _newPos = _tempPos;

            if (Vector3.Distance(_centerPos, _newPos) > patrolRangeMin) break;
        }

        //Debug.Log($"patrol distance , {Vector3.Distance(_centerPos, _newPos)}");
    }    
}
