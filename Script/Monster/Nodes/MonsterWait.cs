using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Wait")]
public class MonsterWait : Wait
{
    public IntReference curState;
    public IntReference monsterType;
    public BoolReference variableToSkip;

    public Animator animator;


    public override void OnEnter()
    {
        //Debug.Log("MonsterWait - OnEnter()");
        base.OnEnter();
        if (monsterType.Value == 1)
        {
            curState.Value = (int)EyeTypeMonsterState.Idle;
            //Debug.Log($"MonsterWait - OnEnter() - curState idle : {(EyeTypeMonsterState)curState.Value}");

            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
        }
        else if (monsterType.Value == 2)
        {
        
        }
        
    }

    public override NodeResult Execute()
    {
        if (variableToSkip.Value)
        {
           //Debug.Log("MonsterWait - Execute() - variableToSkip");
            return NodeResult.failure; 
        }
        return base.Execute();
    }
}
