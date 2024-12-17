using MBT;
using UnityEngine;
[AddComponentMenu("")]
[MBTNode("Example/Monster Attack")]
public class MonsterAttack : Leaf
{
    public BoolReference isGameover;// 매니저로 전역으로 데이터 관리 수정

    public override void OnEnter()
    {
        isGameover.Value = true;
        Debug.Log("MonsterAttack - OnEnter() - 플레이어 사망 - 게임오버");

        base.OnEnter();
    }

    public override NodeResult Execute()
    {
        return NodeResult.success;
    }
}
