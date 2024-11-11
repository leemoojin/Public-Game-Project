using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public PlayerInputAction playerInputs { get; private set; }
    public PlayerInputAction.PlayerActions playerActions { get; private set; }

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
}