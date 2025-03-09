using MBT;
using System.Threading;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Idle")]
public class MonsterIdle : Wait
{
    public IntReference curState;
    public IntReference monsterType;
    public BoolReference skipValueIsDetect;
    public BoolReference canAttack;

    public Monster monster;

    public override void OnEnter()
    {
        //Debug.Log("MonsterWait - OnEnter()");
        base.OnEnter();
        if (monsterType.Value == 1)
        {
            curState.Value = (int)MonsterState.Idle;
            monster.Sound.StopStepAudio();
            monster.SetAnimation(true, false, false, false, false);
        }
        //else if (monsterType.Value == 2)
        //{
        //    curState.Value = (int)EarTypeMonsterState.Idle;
        //    soundSystem.StopStepAudio();
        //    animator.SetBool("Idle", true);
        //    animator.SetBool("Walk", false);
        //    animator.SetBool("Run", false);
        //    animator.SetBool("Focus", false);
        //    //animator.SetBool("Attack", false);
        //}
    }

    public override NodeResult Execute()
    {
        if (skipValueIsDetect.Value || canAttack.Value) return NodeResult.failure;

        return base.Execute();
    }
}
