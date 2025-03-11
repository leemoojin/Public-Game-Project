using MBT;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("")]
[MBTNode("Example/Monster Lost Target")]
public class MonsterLostTarget : Wait
{
    public IntReference curState;
    public BoolReference isDetect;
    public BoolReference isLost;

    public Monster monster;

    private bool _isWait;
    private WaitForSeconds _waitTime = new WaitForSeconds(2.4f);
    private Coroutine _coroutine;

    public override void OnEnter()
    {
        //Debug.Log($"MonsterLostTarget - OnEnter");
        curState.Value = (int)MonsterState.Lost;

        _isWait = true;
        monster.agent.isStopped = true;
        monster.Sound.StopStepAudio();
        monster.SetAnimation(false, false, false, false, true);
        _coroutine = StartCoroutine(WaitTime());
        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        if (!_isWait) 
        {
            if (isDetect.Value) return NodeResult.success;
        }

        return base.Execute();
    }

    private IEnumerator WaitTime()
    {
        yield return _waitTime;
        _isWait = false;
    }

    public override void OnExit()
    {
        StopCoroutine(_coroutine);
        _coroutine = null;
        isLost.Value = false;
        base.OnExit();
    }
}
