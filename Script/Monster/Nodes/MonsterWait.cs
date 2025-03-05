using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Wait")]
public class MonsterWait : Wait
{
    public IntReference curState;
    public IntReference monsterType;
    public BoolReference variableToSkip;
    public BoolReference canAttack;

    public Animator animator;
    public UnitSoundSystem soundSystem;

    public override void OnEnter()
    {
        //Debug.Log("MonsterWait - OnEnter()");
        base.OnEnter();
        if (monsterType.Value == 1)
        {
            curState.Value = (int)EyeTypeMonsterState.Idle;
            soundSystem.StopStepAudio();
            //Debug.Log($"MonsterWait - OnEnter() - curState idle : {(EyeTypeMonsterState)curState.Value}");
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
        }
        else if (monsterType.Value == 2)
        {
            curState.Value = (int)EarTypeMonsterState.Idle;
            soundSystem.StopStepAudio();
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("Focus", false);
            //animator.SetBool("Attack", false);
        }
    }

    public override NodeResult Execute()
    {
        if (variableToSkip.Value || canAttack.Value)
        {
           //Debug.Log("MonsterWait - Execute() - variableToSkip");
            return NodeResult.failure; 
        }
        return base.Execute();
    }
}
