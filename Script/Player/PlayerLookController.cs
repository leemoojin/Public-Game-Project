using Cinemachine;
using UnityEngine;

public class PlayerLookController : MonoBehaviour
{
    [field: Header("HeadMove")]
    [field: SerializeField] public NoiseSettings RunHeadMove { get; private set; }
    [field: SerializeField] public NoiseSettings WalkHeadMove { get; private set; }
    [field: SerializeField] public NoiseSettings CrouchHeadMove { get; private set; }

    public CinemachineVirtualCamera playerVC;
    private CinemachinePOV _pov;
    private CinemachineBasicMultiChannelPerlin noise;
    private Player player;
    private PlayerStateMachine stateMachine;


    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        player = playerTransform.GetComponent<Player>();        
        _pov = playerVC.GetCinemachineComponent<CinemachinePOV>();
        noise = playerVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }

    private void Start()
    {
        stateMachine = player.GetStateMachine();
    }

    private void LateUpdate()
    {
        playerTransform.rotation = Quaternion.Euler(0, _pov.m_HorizontalAxis.Value, 0);

        if (player.CurState == States.RunState)
        {
            noise.m_NoiseProfile = RunHeadMove;
        }
        else if (player.CurState == States.WalkState)
        {
            noise.m_NoiseProfile = WalkHeadMove;
        }
        else if (player.CurState == States.CrouchState)
        {
            noise.m_NoiseProfile = CrouchHeadMove;
        }

        if(stateMachine.IsMoving) noise.m_AmplitudeGain = 1;
        else noise.m_AmplitudeGain = 0;
    }
}
