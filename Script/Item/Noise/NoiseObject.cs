using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class NoiseObject : MonoBehaviour, IInteractable, INoise
    {
        // IInteractable
        [field: SerializeField] public bool IsInteractable { get; set; }// true �϶��� ��ȣ�ۿ밡��
        [field: SerializeField] public float InteractHoldTime { get; set; } = 0f;// 1�� �̻��϶� Ȧ�����ͷ�Ʈ ����
        [field: SerializeField] public string ObjectName { get; set; }// SO
        [field: SerializeField] public string InteractKey { get; set; }// SO
        [field: SerializeField] public string InteractType { get; set; }// SO

        // INoise
        public float CurNoiseAmount { get; set; }
        public float SumNoiseAmount { get; set; }
        public float DecreaseSpeed { get; set; }
        public float MaxNoiseAmount { get; set; } = 20f;

        public List<ObjectSoundData> SoundList;
        private AudioSource AS;


        public void Interact()
        {
            //Debug.Log("TempInteractObject - ��ȣ�ۿ� ����");
            //IsInteractable = false;


        }

        public void ActivateInteraction()
        {

        }
    }
}


