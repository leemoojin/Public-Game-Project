using UnityEngine;

namespace NPC
{
    public class TutorialNPC: MonoBehaviour, IInteractable
    {

        // IInteractable
        [field: SerializeField] public bool IsInteractable { get; set; }
        [field: SerializeField] public float InteractHoldTime { get; set; } = 0f;

        // Player UI
        [field: SerializeField] public string ObjectName { get; set; }
        [field: SerializeField] public string InteractKey { get; set; }
        [field: SerializeField] public string InteractType { get; set; }

        public void Interact()
        {
            Debug.Log("NPCInteract - NPC와 상호작용 성공");
            
            IsInteractable = false;
            shutter.IsInteractable = true;
            shutter.EBtn.SetActive(true);
            Destroy(EBtn);
        }

        [SerializeField] private Shutter shutter;
        [SerializeField] private GameObject EBtn;
    }
}