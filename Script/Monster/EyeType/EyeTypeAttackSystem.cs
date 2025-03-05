using MBT;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EyeTypeAttackSystem : MonoBehaviour
{
    public Blackboard bb;
    public IAttackable cachedPlayer;
    public IAttackable cachedNPC;

    private IAttackable _target;
    private Coroutine _npcAttackCorutine;
    public bool reWork = true;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"hit");
        if (bb.GetVariable<Variable<int>>("curState").Value != (int)EyeTypeMonsterState.Attack) return;
        reWork = false;

        if (other.gameObject.layer == 7) 
        {
            reWork = true;
            StartNpcAttack(other);
            return;
        }

        if (other.gameObject.layer == 13)
        {
            StartNpcAttack(other);
            return;
        }
    }

    private IEnumerator NpcAttack(Collider other)
    {
        if (cachedNPC != null)
        {
            Attack(cachedNPC);
        }

        if (cachedNPC == null && other.TryGetComponent<IAttackable>(out var attackableTarget))
        {
            _target = attackableTarget;
            Attack(_target);
        }

        yield return null;

        //Debug.Log("EyeTypeAttackSystem - OnTriggerEnter");
        if (reWork)
        {
            bb.GetVariable<Variable<bool>>("isWork").Value = true;
            bb.GetVariable<Variable<bool>>("isAttacking").Value = false;
        }
    }

    private void StartNpcAttack(Collider other)
    {
        StopNpcAttack();
        _npcAttackCorutine = StartCoroutine(NpcAttack(other));
    }

    private void StopNpcAttack()
    {
        if (_npcAttackCorutine != null) StopCoroutine(_npcAttackCorutine);
    }

    public void Attack()
    {
        if (_target != null)
        {
            _target.OnHitSuccess();
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
