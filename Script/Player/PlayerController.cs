using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public PlayerInputAction playerInputs { get; private set; }
    public PlayerInputAction.PlayerActions playerActions { get; private set; }
    public PlayerInputAction.UIActions UIActions { get; private set; }

    private void Awake()
    {
        playerInputs = new PlayerInputAction();
        playerActions = playerInputs.Player;
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }

    public void OnPlayerAction()
    {
        playerActions.Enable();
        UIActions.Disable();
    }

    public void OnUiAction()
    {
        UIActions.Enable();
        playerActions.Disable();
    }
}