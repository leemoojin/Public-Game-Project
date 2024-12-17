using UnityEngine;

namespace NPC
{
    public class NPCInteract : MonoBehaviour, IInteractable
    {

        // IInteractable
        [field: SerializeField] public bool IsInteractable { get; set; }// true 일때만 상호작용가능
        [field: SerializeField] public float InteractHoldTime { get; set; } = 0f;// 1초 이상일때 홀드인터렉트 진행

        // Player UI
        [field: SerializeField] public string ObjectName { get; set; }// SO
        [field: SerializeField] public string InteractKey { get; set; }// SO
        [field: SerializeField] public string InteractType { get; set; }// SO


        public void Interact()
        {
            Debug.Log("NPCInteract - NPC와 상호작용 성공");


            // 대화 스크립트를 출력하는 메서드 호출하면 됨
            // 1회성 대화일 경우 대화 후 IsInteractable 값을 false 로 변경할 것
            // 반복이 가능한 대화일 경우 IsInteractable 값을 true 로 유지
        }
    }
}


