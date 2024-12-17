using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class NoiseObject : MonoBehaviour, IInteractable, INoise
    {
        // IInteractable
        [field: SerializeField] public bool IsInteractable { get; set; }// true 일때만 상호작용가능
        [field: SerializeField] public float InteractHoldTime { get; set; } = 0f;// 1초 이상일때 홀드인터렉트 진행
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
            //Debug.Log("TempInteractObject - 상호작용 성공");
            //IsInteractable = false;


        }

        public void ActivateInteraction()
        {

        }
    }
}


