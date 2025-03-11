using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EyeTypeMonsterDataSO", menuName = "SO/Unit/Monster/EyeType")]
public class EyeTypeMonsterDataSO : ScriptableObject
{
    [field: SerializeField] public EyeTypeData Data { get; private set; }

    [Serializable]
    public class EyeTypeData
    {
        [field: Header("Detect")]
        [field: SerializeField][field: Range(0f, 50f)] public float FindRange { get; private set; } = 12.5f;
        [field: SerializeField][field: Range(0f, 50f)] public float ChaseRange { get; private set; } = 17.5f;
        [field: SerializeField][field: Range(0f, 300f)] public float ViewAngle { get; private set; } = 120f;

        [field: Header("MoveSpeed")]
        [field: SerializeField][field: Range(1f, 10f)] public float BaseSpeed { get; private set; } = 4f;
        [field: SerializeField][field: Range(0.1f, 20f)] public float WalkSpeedModifier { get; private set; } = 0.7f;
        [field: SerializeField][field: Range(0.1f, 40f)] public float RunSpeedModifier { get; private set; } = 2f;

        [field: Header("Attack")]
        [field: SerializeField][field: Range(1.5f, 10f)] public float AttackRange { get; private set; } = 2f;
    }
}
