using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "SO/Unit/Player")]
public class PlayerDataSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }

}

[Serializable]
public class PlayerGroundData
{
    [field: Header("BaseData")]
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 15f;
    [field: SerializeField][field: Range(0f, 100f)] public float GravityModifier { get; private set; } = 30f;


    [field: Header("WalkData")]
    [field: SerializeField][field: Range(0f, 1f)] public float WalkSpeedModifier { get; private set; } = 1f;

    [field: Header("RunData")]
    [field: SerializeField][field: Range(0f, 4f)] public float RunSpeedModifier { get; private set; } = 1.8f;

    [field: Header("CrouchData")]
    [field: SerializeField][field: Range(0f, 2f)] public float CrouchSpeedModifier { get; private set; } = 0.2f;
    [field: SerializeField][field: Range(0f, 1f)] public float CrouchHeight { get; private set; } = 0.6f;

}
