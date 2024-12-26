using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterDataSO", menuName = "SO/Unit/Monster")]
public class MonsterDataSO : ScriptableObject
{
    [field: SerializeField] public EyeTypeData Data { get; private set; }

    [Serializable]
    public class EyeTypeData
    {
        [field: Header("BaseData")]
        [field: SerializeField][field: Range(0f, 200f)] public float FindRange { get; private set; } = 30f;
        [field: SerializeField][field: Range(0f, 200f)] public float ChaseRange { get; private set; } = 30f;

        [field: SerializeField][field: Range(0f, 200f)] public float ViewAngle { get; private set; } = 120f;
        [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 15f;

        [field: Header("WalkData")]
        [field: SerializeField][field: Range(0f, 1f)] public float WalkSpeedModifier { get; private set; } = 1f;

        [field: Header("RunData")]
        [field: SerializeField][field: Range(0f, 4f)] public float RunSpeedModifier { get; private set; } = 1.8f;

    }
}
