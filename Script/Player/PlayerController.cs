using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [field: Header("References")]
    [SerializeField] private InputController Input { get; set; }

    private void Awake()
    {
        Input.Init();
    }
}
