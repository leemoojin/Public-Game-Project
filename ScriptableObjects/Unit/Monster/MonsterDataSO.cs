using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterDataSO", menuName = "SO/Unit/Monster")]
public class MonsterDataSO : ScriptableObject
{
    [field: SerializeField] public EyeTypeData EyeType { get; private set; }
    [field: SerializeField] public EarTypeData EarType { get; private set; }


    [Serializable]
    public class EyeTypeData
    {
        [field: Header("BaseData")]
        [field: SerializeField][field: Range(0f, 200f)] public float FindRange { get; private set; } = 30f;
        [field: SerializeField][field: Range(0f, 200f)] public float ChaseRange { get; private set; } = 30f;

        [field: SerializeField][field: Range(0f, 200f)] public float ViewAngle { get; private set; } = 120f;
        [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 15f;

        [field: Header("WalkData")]
        [field: SerializeField][field: Range(0f, 5f)] public float WalkSpeedModifier { get; private set; } = 1f;

        [field: Header("RunData")]
        [field: SerializeField][field: Range(0f, 5f)] public float RunSpeedModifier { get; private set; } = 1.8f;

    }

    [Serializable]
    public class EarTypeData
    {
        [field: Header("BaseData")]
        [field: SerializeField][field: Range(0f, 150f)] public float DetectRange { get; private set; } = 60f;
        [field: SerializeField][field: Range(0f, 30f)] public float DetectNoiseMax { get; private set; } = 9f;
        [field: SerializeField][field: Range(0f, 30f)] public float DetectNoiseMin { get; private set; } = 2.5f;

        [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 4f;

        [field: Header("WalkData")]
        [field: SerializeField][field: Range(0f, 5f)] public float WalkSpeedModifier { get; private set; } = 1f;

        [field: Header("RunData")]
        [field: SerializeField][field: Range(0f, 5f)] public float RunSpeedModifier { get; private set; } = 1.8f;

        [field: Header("AttackData")]
        [field: SerializeField][field: Range(0f, 20f)] public float AttackRange { get; private set; } = 10f;


    }
}
