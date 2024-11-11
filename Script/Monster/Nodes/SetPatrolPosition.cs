using MBT;
using UnityEngine;


[MBTNode("Example/Set Patrol Position")]
[AddComponentMenu("")]
public class SetPatrolPosition : Leaf
{
    public Vector3Reference variableToSetPatrolPos = new Vector3Reference(VarRefMode.DisableConstant);
    public Vector3Reference variableToSetOriginPos;
    public TransformReference self;
    public TransformReference variableToFailure;
    public bool startOriginPos;
    public float patrolRangeMin = 30f;
    public float patrolRangeMax = 50f;


    private Vector3 _centerPos;
    private Vector3 _newPos;
    private Vector3 _tempPos;

    public override void OnEnter()
    {
        base.OnEnter();
        if (variableToSetOriginPos.Value == Vector3.zero) variableToSetOriginPos.Value = self.Value.position;
    }

    public override NodeResult Execute()
    {
        if(variableToFailure.Value != null) return NodeResult.failure;

        GetPosition();
        if (_newPos != Vector3.zero)
        {
            variableToSetPatrolPos.Value = _newPos;
            //Debug.Log($"이동거리 : {Vector3.Distance(_centerPos, _newPos)}");
            return NodeResult.success;
        }
        return NodeResult.failure;
    }

    private void GetPosition()
    {
        if (startOriginPos) _centerPos = variableToSetOriginPos.Value;
        else _centerPos = self.Value.position;

        _newPos = Vector3.zero;
        _tempPos = Vector3.zero;

        for (int i = 0; i < 50; i++)
        {
            _tempPos = Random.insideUnitSphere * patrolRangeMax;
            _tempPos.y = 0;
            _tempPos += _centerPos;

            if(_newPos == Vector3.zero) _newPos = _tempPos;
            else if (Vector3.Distance(_centerPos, _newPos) > Vector3.Distance(_centerPos, _tempPos)) _newPos = _tempPos;

            if (Vector3.Distance(_centerPos, _newPos) > patrolRangeMin) break;
        }
    }    
}
