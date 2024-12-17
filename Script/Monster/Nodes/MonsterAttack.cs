using MBT;
using UnityEngine;
[AddComponentMenu("")]
[MBTNode("Example/Monster Attack")]
public class MonsterAttack : Leaf
{
    public BoolReference isGameover;// �Ŵ����� �������� ������ ���� ����

    public override void OnEnter()
    {
        isGameover.Value = true;
        Debug.Log("MonsterAttack - OnEnter() - �÷��̾� ��� - ���ӿ���");

        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        return NodeResult.success;
    }
}
