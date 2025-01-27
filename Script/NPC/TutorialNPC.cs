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
            IsInteractable = false;
            
            Destroy(EBtn);
            UIManager.Instance.DialogueOpen(false);
            UIManager.Instance.dialogueEnd += IntereactEBtn;
        }

        [SerializeField] private Shutter shutter;
        [SerializeField] private GameObject EBtn;

        void IntereactEBtn()
        {
            shutter.IsInteractable = true;
            shutter.EBtn.SetActive(true);
        }
    }
}