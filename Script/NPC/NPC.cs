using MBT;
using System;
using UnityEditor;
using UnityEngine;

namespace NPC
{
    public class NPC : MonoBehaviour, IInteractable
    {
        [field: Header("References")]
        [field: SerializeField] public NPCDataSO NPCData { get; private set; }
        public Blackboard BB;

        [field: Header("Interactable")]
        [field: SerializeField] public bool IsInteractable { get; set; }
        [field: SerializeField] public float InteractHoldTime { get; set; } = 0f;
        public NPCInteract NPCInteract;

        [field: Header("State")]
        public NPCState curState;

        // Player UI
        public string ObjectName { get; set; }
        public string InteractKey { get; set; }
        public string InteractType { get; set; }


        private void Start()
        {
            ObjectName = NPCData.Data.NPCName;
            InteractKey = NPCData.Data.InteractKey;
            InteractType = NPCData.Data.InteractType;

            //Debug.Log($"npc key, {_blackboard.variables[0].key}");          
            //Debug.Log($"npc GetVariable, {_blackboard.GetVariable<Variable<bool>>(_blackboard.variables[0].key).Value}");

            //NPCState state = NPCState.Move | NPCState.Dead;
            //curState = state;
            //Debug.Log($"state, {state}");          
        }

        public void Interact()
        {
            //Debug.Log("NPC - NPC와 상호작용 성공");

            if ((NPCInteract & NPCInteract.CanRepeat) != NPCInteract.CanRepeat) IsInteractable = false;

            if ((NPCInteract & NPCInteract.CanTalk) == NPCInteract.CanTalk)
            {
                Debug.Log("NPC - Interact() - 대화 시작");
            }

            if ((NPCInteract & NPCInteract.CanFollow) == NPCInteract.CanFollow)
            {
                Debug.Log("NPC - Interact() - 팔로우 시작");
                BB.GetVariable<Variable<bool>>("isFollow").Value = true;
            }

        }
    }
}


