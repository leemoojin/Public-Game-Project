using UnityEngine;
public class PlayerControllerTemp : MonoBehaviour
{
    public PlayerInputAction playerInputs { get; private set; }
    public PlayerInputAction.PlayerActions playerActions { get; private set; }
    public PlayerInputAction.UIActions uiActions { get; private set; }

    private void Awake()
    {
        playerInputs = new PlayerInputAction();
        playerActions = playerInputs.Player;
        uiActions = playerInputs.UI;
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
        uiActions.Disable();
    }

    public void OnUiAction()
    {
        uiActions.Enable();
        playerActions.Disable();
    }
}