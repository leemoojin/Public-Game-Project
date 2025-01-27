using MBT;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NPCDay1JHS : MonoBehaviour, IInteractable, IAttackable
    {
        [field: Header("References")]
        [field: SerializeField] private NPCDataSO NpcData { get; set; }
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

        // player ui
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

            // 이벤트 매니저에서 흉기 획득 처리가 되었을 때 -> NPC 사망로직 추가 할 것 (하이드 루트)

            // 이벤트 매니저에서 탐색 완료 처리가 되었을 때 -> NPC Player follow 로직 추가 (지킬 루트)

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
                IsInteractable = false;
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

