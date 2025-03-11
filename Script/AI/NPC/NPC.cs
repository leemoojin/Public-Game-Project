using MBT;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NPC : MonoBehaviour, IInteractable, IAttackable
    {
        [field: Header("References")]
        [field: SerializeField] public NPCDataSO NpcData { get; private set; }
        public Blackboard bb;
        public Transform player;
        public UnitSoundSystem soundSystem;
        public NavMeshAgent agent;
        public CapsuleCollider capsuleCollider;

        [field: Header("Animations")]
        public Animator animator;

        [field: Header("Interactable")]
        [field: SerializeField] public bool IsInteractable { get; set; }
        [field: SerializeField] public float InteractHoldTime { get; set; } = 0f;
        
        [field: Header("Setting")]
        public NPCSetting npcSetting;

        public string ObjectName { get; set; }
        public string InteractKey { get; set; }
        public string InteractType { get; set; }

        private Transform _self;
        private bool _hasReachedPlayer = false;
        private bool _lookAtPlayer = false;
        

        private void Start()
        {
            ObjectName = NpcData.Data.NPCName;
            InteractKey = NpcData.Data.InteractKey;
            InteractType = NpcData.Data.InteractType;
            animator.SetBool("Idle", true);
            _self = transform;

            if (bb != null)
            {
                bb.GetVariable<Variable<float>>("nearDistance").Value = NpcData.Data.NearDistance;
                bb.GetVariable<Variable<float>>("farDistance").Value = NpcData.Data.FarDistance;
                bb.GetVariable<Variable<float>>("baseSpeed").Value = NpcData.Data.BaseSpeed;
                bb.GetVariable<Variable<float>>("walkModifier").Value = NpcData.Data.WalkSpeedModifier;
                bb.GetVariable<Variable<float>>("runModifier").Value = NpcData.Data.RunSpeedModifier;
                bb.GetVariable<Variable<float>>("crouchModifier").Value = NpcData.Data.CrouchSpeedModifier;
                bb.GetVariable<Variable<int>>("curState").Value = (int)NPCState.Idle;
            }

            //soundSystem.PlaySoundTemp();
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

            if ((npcSetting & NPCSetting.CanRepeat) != NPCSetting.CanRepeat) IsInteractable = false;

            if ((npcSetting & NPCSetting.CanTalk) == NPCSetting.CanTalk)
            {
                // NPC dialog
            }

            if ((npcSetting & NPCSetting.HaveDestination) == NPCSetting.HaveDestination)
            {
                // move
            }

            if ((npcSetting & NPCSetting.CanFollow) == NPCSetting.CanFollow && !bb.GetVariable<Variable<bool>>("isFollow").Value)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("@Follow", true);
              
                bb.GetVariable<Variable<bool>>("isFollow").Value = true;
            }
        }

        private void LookAtPlayer()
        {     
            Vector3 direction = player.position - _self.position;

            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                _self.rotation = Quaternion.Lerp(
                    _self.rotation,
                    targetRotation,
                    Time.deltaTime * 2f
                );

                float angleDifference = Quaternion.Angle(_self.rotation, targetRotation);
                if (angleDifference <= 1f)
                {
                    _hasReachedPlayer = true;
                    _lookAtPlayer = false;
                }
            }
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.layer == 8 && BB.GetVariable<Variable<int>>("curState").Value != (int)NPCState.Dead)
        //    {
        //        NPCDead();
        //    }
        //}

        private void NPCDead()
        {
            gameObject.layer = 0;
            capsuleCollider.enabled = false;
            bb.GetVariable<Variable<int>>("curState").Value = (int)NPCState.Dead;
            bb.GetVariable<Variable<bool>>("isFollow").Value = false;
            animator.SetBool("@Follow", false);
            animator.SetBool("@Crouch", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("Dead", true);
            soundSystem.StopStepAudio();
            soundSystem.OtherSoundPlay("Scream");
        }

        public void OnHitSuccess()
        {
            if (bb.GetVariable<Variable<int>>("curState").Value != (int)NPCState.Dead)
            {
                NPCDead();
            }
        }

        public void NpcStop()
        {
            bb.GetVariable<Variable<bool>>("isFollow").Value = false;
            agent.isStopped = true;
            animator.SetBool("@Follow", false);
            animator.SetBool("@Crouch", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("Dead", false);
        }
    }
}


