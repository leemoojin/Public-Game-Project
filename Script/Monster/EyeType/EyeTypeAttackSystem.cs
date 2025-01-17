using MBT;
using UnityEngine;

public class EyeTypeAttackSystem : MonoBehaviour
{
    public Blackboard bb;
    public IAttackable cachedPlayer;
    public IAttackable cachedNPC;

    private IAttackable target;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"hit");
        if (bb.GetVariable<Variable<int>>("curState").Value != (int)EyeTypeMonsterState.Attack) return;

        if (other.gameObject.layer == 7) 
        {
            if (cachedNPC != null)
            {
                Attack(cachedNPC);
            }

            if (cachedNPC == null && other.TryGetComponent<IAttackable>(out var attackableTarget))
            {
                target = attackableTarget;
                Attack(target);
            }

            //bb.GetVariable<Variable<bool>>("isWork").Value = true;
            //bb.GetVariable<Variable<bool>>("isAttacking").Value = false;
            //bb.GetVariable<Variable<bool>>("isDetect").Value = false;
            //bb.GetVariable<Variable<Transform>>("target").Value = null;
            return;
        }
        
        //fail, rework
        //bb.GetVariable<Variable<bool>>("isWork").Value = true;
    }

    public void Attack()
    {
        if (target != null)
        {
            target.OnHitSuccess();
        }
    }

    public void Attack(IAttackable target)
    {
        if (target != null)
        {
            target.OnHitSuccess();
        }
    }
}
