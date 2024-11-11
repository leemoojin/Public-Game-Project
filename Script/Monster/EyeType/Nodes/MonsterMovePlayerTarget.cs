using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Player Target")]
public class MonsterMovePlayerTarget : MoveToVector
{
    public float moveSpeed = 15f;

    public override void OnEnter()
    {
        agent.speed = moveSpeed;
        base.OnEnter();
    }
}
