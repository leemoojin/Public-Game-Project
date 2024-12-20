using MBT;
using System;
using UnityEditor;
using UnityEngine;

namespace NPC
{
    public class NPCInteract : MonoBehaviour, IInteractable
    {
        [field: Header("References")]
        [field: SerializeField] public NPCDataSO NPCData { get; private set; }

        // IInteractable
        [field: Header("Interactable")]
        [field: SerializeField] public bool IsInteractable { get; set; }// true 일때만 상호작용가능
        [field: SerializeField] public float InteractHoldTime { get; set; } = 0f;// 1초 이상일때 홀드인터렉트 진행

        [field: Header("Interactable")]
        [SerializeField] private string currentState;

        // Player UI
        public string ObjectName { get; set; }// SO
        public string InteractKey { get; set; }// SO
        public string InteractType { get; set; }// SO

        //
        public GameObject BT;
        private Blackboard _blackboard;

        private void Awake()
        {
            _blackboard = BT.GetComponent<Blackboard>();
        }

        private void Start()
        {
            ObjectName = NPCData.Data.NPCName;
            InteractKey = NPCData.Data.InteractKey;
            InteractType = NPCData.Data.InteractType;

            //Debug.Log($"npc key, {_blackboard.variables[0].key}");          
            //Debug.Log($"npc GetVariable, {_blackboard.GetVariable<Variable<bool>>(_blackboard.variables[0].key).Value}");

            NPCState state = NPCState.Move | NPCState.Dead;
            currentState = state.ToString();

            Debug.Log($"state, {state}");          
        }

        public void Interact()
        {
            Debug.Log("NPCInteract - NPC와 상호작용 성공");
            IsInteractable = false;
            _blackboard.GetVariable<Variable<bool>>("isFollow").Value = true;

            // 대화 스크립트를 출력하는 메서드 호출하면 됨
            // 1회성 대화일 경우 대화 후 IsInteractable 값을 false 로 변경할 것
            // 반복이 가능한 대화일 경우 IsInteractable 값을 true 로 유지
        }
    }
}


