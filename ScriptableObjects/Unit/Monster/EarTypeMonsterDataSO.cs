using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EarTypeMonsterDataSO", menuName = "SO/Unit/Monster/EarType")]
public class EarTypeMonsterDataSO : ScriptableObject
{
    [field: SerializeField] public EarTypeData Data { get; private set; }

    [Serializable]
    public class EarTypeData
    {
        [field: Header("Detect")]
        [field: SerializeField][field: Range(10f, 150f)] public float DetectRangeMax { get; private set; } = 60f;
        [field: SerializeField][field: Range(10f, 100f)] public float DetectRangeMin { get; private set; } = 30f;
        [field: SerializeField][field: Range(1f, 30f)] public float DetectNoiseMax { get; private set; } = 9f;
        [field: SerializeField][field: Range(1f, 30f)] public float DetectNoiseMin { get; private set; } = 2.5f;

        [field: Header("MoveSpeed")]
        [field: SerializeField][field: Range(1f, 10f)] public float BaseSpeed { get; private set; } = 1.3f;
        [field: SerializeField][field: Range(0.1f, 20f)] public float WalkSpeedModifier { get; private set; } = 0.6f;
        [field: SerializeField][field: Range(0.1f, 40f)] public float RunSpeedModifier { get; private set; } = 14f;

        [field: Header("Attack")]
        [field: SerializeField][field: Range(3.1f, 10f)] public float AttackRange { get; private set; } = 3.1f;
    }
}
