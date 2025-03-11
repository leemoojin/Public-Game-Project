using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Attack Range Check Service")]
public class AttackRangeCheckService : Service
{
    public TransformReference player;
    public FloatReference attackRange;
    public BoolReference canAttack;
    public BoolReference isNoiseDetect;
    public BoolReference isWork;


    private Transform _player;
    private float _attackRange;

    public override void OnEnter()
    {
        //Debug.Log($"AttackRangeCheckService - OnEnter - isWork : {isWork.Value}, {transform.position}");
        _player = player.Value;
        _attackRange = attackRange.Value;
        base.OnEnter();        
    }

    public override void Task()
    {

        if (_attackRange >= Vector3.Distance(transform.position, _player.position))
        {
            canAttack.Value = true;
            //isWork.Value = false;
            isNoiseDetect.Value = false;

        }
        else 
        {
            canAttack.Value = false;
        }
    }

    public override void OnExit()
    {
        //Debug.Log($"AttackRangeCheckService - OnExit isWork : {isWork.Value}");
        base.OnExit();

    }
}
