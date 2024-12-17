using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NPCUnit : MonoBehaviour, IInteractable
    {
        // IInteractable
        [field: SerializeField] public bool IsInteractable { get; set; }// true 일때만 상호작용가능
        [field: SerializeField] public float InteractHoldTime { get; set; } = 0f;// 1초 이상일때 홀드인터렉트 진행
        // Player UI
        [field: SerializeField] public string ObjectName { get; set; }// SO
        [field: SerializeField] public string InteractKey { get; set; }// SO
        [field: SerializeField] public string InteractType { get; set; }// SO

        public bool canTalk;
        public bool canFollow;
        private bool _isMoving;
        private bool _isArrive;

        public NavMeshAgent agent;
        public GameObject player;// 추후 전역 접근으로 수정
        public float distanceToPlayer;

        public Transform arrivalPoint;// npc를 데려가야하는 목표 지역


        public void Interact()
        {
            //Debug.Log("NPCInteract - NPC와 상호작용 성공");

            // 대화 스크립트를 출력하는 메서드 호출하면 됨
            // 1회성 대화일 경우 대화 후 IsInteractable 값을 false 로 변경할 것
            // 반복이 가능한 대화일 경우 IsInteractable 값을 true 로 유지

            if (_isArrive)
            {
                Debug.Log("NPCInteract - 목적지에 도착한 상태 NPC와 대화");
                return;
            }


            // npc idle
            if (!_isMoving)
            {
                if (canTalk)
                {
                    Debug.Log("NPCInteract - 대기중일때 NPC와 대화");
                }

                if (canFollow)
                {
                    FollowPlayer();
                }
            }
            else
            {
                // npc player follow

                if (canTalk)
                {
                    Debug.Log("NPCInteract - 이동중일때 NPC와 대화");
                }
            }
        }

        private void FollowPlayer()
        {
            //Debug.Log("NPCInteract - FollowPlayer()");

            _isMoving = true;
            InteractType = "Talk";
            agent.SetDestination(player.transform.position);
        }

        private void Update()
        {
            if (!_isMoving) return;

            //agent.SetDestination(player.transform.position);

            if (agent.pathPending) return;

            if (agent.remainingDistance < distanceToPlayer)
            {
                agent.isStopped = true;
            }

            if (Vector3.Distance(transform.position, player.transform.position) > distanceToPlayer)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
            }

            if (Vector3.Distance(transform.position, arrivalPoint.position) < 4f)
            {
                agent.isStopped = true;
                _isMoving = false;
                _isArrive = true;
                Debug.Log("NPCInteract - Update() - 도착");
            }

        }

        private void OnTriggerEnter(Collider other)
        {
                //Debug.Log("NPCInteract - OnTriggerEnter() - 충돌");

        }

        
    }
}


