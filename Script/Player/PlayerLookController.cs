using Cinemachine;
using UnityEngine;

public class PlayerLookController : MonoBehaviour
{
    [field: Header("HeadMove")]
    [field: SerializeField] public NoiseSettings RunHeadMove { get; private set; }
    [field: SerializeField] public NoiseSettings WalkHeadMove { get; private set; }
    [field: SerializeField] public NoiseSettings CrouchHeadMove { get; private set; }

    [field: Header("References")]
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] private Transform playerTransform;
    public CinemachineVirtualCamera playerVC;

    private CinemachinePOV _pov;
    private CinemachineBasicMultiChannelPerlin _noise;
    private PlayerStateMachine _stateMachine;

    private void Awake()
    {
        _pov = playerVC.GetCinemachineComponent<CinemachinePOV>();
        _noise = playerVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        _stateMachine = Player.GetStateMachine();
    }

    private void LateUpdate()
    {
        playerTransform.rotation = Quaternion.Euler(0, _pov.m_HorizontalAxis.Value, 0);

        if (Player.CurState == PlayerEnum.PlayerState.RunState)
        {
            _noise.m_NoiseProfile = RunHeadMove;
        }
        else if (Player.CurState == PlayerEnum.PlayerState.WalkState)
        {
            _noise.m_NoiseProfile = WalkHeadMove;
        }
        else if (Player.CurState == PlayerEnum.PlayerState.CrouchState)
        {
            _noise.m_NoiseProfile = CrouchHeadMove;
        }

        if(_stateMachine.IsMoving) _noise.m_AmplitudeGain = 1;
        else _noise.m_AmplitudeGain = 0;
    }
}
