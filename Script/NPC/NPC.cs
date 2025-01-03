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
        public Transform player;
        public Transform self;

        [field: Header("Animations")]
        public Animator animator;

        [field: Header("Interactable")]
        [field: SerializeField] public bool IsInteractable { get; set; }
        [field: SerializeField] public float InteractHoldTime { get; set; } = 0f;
        public NPCInteract NPCInteract;

        [field: Header("State")]
        public NPCState curState;

        // SO
        // Player UI
        public string ObjectName { get; set; }
        public string InteractKey { get; set; }
        public string InteractType { get; set; }

        private bool _hasReachedPlayer = false;
        private bool _lookAtPlayer = false;
        

        private void Start()
        {
            ObjectName = NPCData.Data.NPCName;
            InteractKey = NPCData.Data.InteractKey;
            InteractType = NPCData.Data.InteractType;
            curState = NPCState.Idle;
            animator.SetBool("Idle", true);
            self = transform;

            if (BB != null)
            {
                BB.GetVariable<Variable<float>>("nearDistance").Value = NPCData.Data.NearDistance;
                BB.GetVariable<Variable<float>>("farDistance").Value = NPCData.Data.FarDistance;
                BB.GetVariable<Variable<float>>("baseSpeed").Value = NPCData.Data.BaseSpeed;
                BB.GetVariable<Variable<float>>("walkModifier").Value = NPCData.Data.WalkSpeedModifier;
                BB.GetVariable<Variable<float>>("runModifier").Value = NPCData.Data.RunSpeedModifier;
                BB.GetVariable<Variable<float>>("crouchModifier").Value = NPCData.Data.CrouchSpeedModifier;
                BB.GetVariable<Variable<int>>("curState").Value = (int)curState;
            }
        }

        private void Update()
        {
            if (!_hasReachedPlayer && _lookAtPlayer)
            {
                LookAtPlayer();
            }
        }

        public void Interact()
        {
            _hasReachedPlayer = false;
            _lookAtPlayer = true;

            if ((NPCInteract & NPCInteract.CanRepeat) != NPCInteract.CanRepeat) IsInteractable = false;

            if ((NPCInteract & NPCInteract.CanTalk) == NPCInteract.CanTalk)
            {
                // NPC dialog
            }

            if ((NPCInteract & NPCInteract.HaveDestination) == NPCInteract.HaveDestination)
            {
                
            }

            if ((NPCInteract & NPCInteract.CanFollow) == NPCInteract.CanFollow && !BB.GetVariable<Variable<bool>>("isFollow").Value)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("@Follow", true);
              
                BB.GetVariable<Variable<bool>>("isFollow").Value = true;
            }
        }

        private void LookAtPlayer()
        {     
            Vector3 direction = player.position - self.position;

            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                self.rotation = Quaternion.Lerp(
                    self.rotation,
                    targetRotation,
                    Time.deltaTime * 2f
                );

                float angleDifference = Quaternion.Angle(self.rotation, targetRotation);
                if (angleDifference <= 0.2f)
                {
                    _hasReachedPlayer = true;
                    _lookAtPlayer = false;
                }
            }
        }

        

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 8 && curState != NPCState.Dead)
            {
                NPCDead();
            }
        }

        private void NPCDead()
        {
            gameObject.layer = 0;
            curState = NPCState.Dead;
            BB.GetVariable<Variable<int>>("curState").Value = (int)curState;
            BB.GetVariable<Variable<bool>>("isFollow").Value = false;
            animator.SetBool("@Follow", false);
            animator.SetBool("@Crouch", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("Dead", true);

        }


    }
}


