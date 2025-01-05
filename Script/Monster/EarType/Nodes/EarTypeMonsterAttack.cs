using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/EarType Monster Attack")]
public class EarTypeMonsterAttack : Leaf
{
    public IntReference curState;


    public override void OnEnter()
    {
        //Debug.Log("EarTypeMonsterAttack - OnEnter()");
        base.OnEnter();

        //animator.SetBool("Run", false);
        //animator.SetBool("Walk", false);
        //animator.SetBool("Idle", false);
        //animator.SetBool("Attack", true);
    }

    public override NodeResult Execute()
    {
        throw new System.NotImplementedException();
    }
}
