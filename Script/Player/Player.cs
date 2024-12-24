using UnityEngine;

public class Player : MonoBehaviour, INoise
{
    [field: Header("References")]
    [field: SerializeField] public PlayerDataSO Data { get; private set; }

    [field: Header("State")]
    [field: SerializeField] public PlayerEnum.PlayerState CurState { get; set; }

    // INoise
    [field: Header("Noise")]
    [field: SerializeField] public float CurNoiseAmount { get; set; }
    public float SumNoiseAmount { get; set; }
    public float DecreaseSpeed { get; set; } = 5.0f;
    public float MaxNoiseAmount { get; set; } = 13f;

    //Karma
    [field: Header("Karma")]
    [field: SerializeField] public float KarmaPoint { get; set; }



    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public FootstepsSystem FootstepsSystem { get; private set; }
    public PlayerInteractable PlayerInteractable { get; private set; }

    // InputAction
    public PlayerController Input { get; private set; }

    private PlayerStateMachine _stateMachine;

    private void Awake()
    {
        //Debug.Log("Player - Awake()");

        Controller = GetComponent<CharacterController>();
        Input = GetComponent<PlayerController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        FootstepsSystem = GetComponent<FootstepsSystem>();
        PlayerInteractable = GetComponent<PlayerInteractable>();

        _stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);

    }

    private void Update()
    {

        _stateMachine.HandleInput();
        _stateMachine.Update();

        CheckNoise();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }

    private void CheckNoise()
    {
        if (CurNoiseAmount > 0)
        {
            CurNoiseAmount -= DecreaseSpeed * Time.deltaTime;
            if (CurNoiseAmount <= 0) CurNoiseAmount = 0;
        }

        if (CurNoiseAmount >= MaxNoiseAmount) CurNoiseAmount = MaxNoiseAmount;
    }

    public PlayerStateMachine GetStateMachine() { return _stateMachine; }
}
