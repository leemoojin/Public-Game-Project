using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDataSO", menuName = "SO/Unit/NPC")]
public class NPCDataSO : ScriptableObject
{
    [field: SerializeField] public NPCData Data { get; private set; }


    [Serializable]
    public class NPCData
    {
        [field: Header("UIData")]
        [field: SerializeField] public string NPCName { get; private set; }
        [field: SerializeField] public string InteractKey { get; private set; }
        [field: SerializeField] public string InteractType { get; private set; }

        [field: Header("BaseData")]
        [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 15f;
        [field: SerializeField][field: Range(0f, 25f)] public float NearDistance { get; private set; }
        [field: SerializeField][field: Range(0f, 25f)] public float FarDistance { get; private set; }

        [field: Header("WalkData")]
        [field: SerializeField][field: Range(0f, 1f)] public float WalkSpeedModifier { get; private set; } = 1f;

        [field: Header("RunData")]
        [field: SerializeField][field: Range(0f, 4f)] public float RunSpeedModifier { get; private set; } = 1.8f;

        [field: Header("CrouchData")]
        [field: SerializeField][field: Range(0f, 2f)] public float CrouchSpeedModifier { get; private set; } = 0.2f;



    }

}
