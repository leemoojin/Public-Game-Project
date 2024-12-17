using UnityEngine;
using DG.Tweening;

public class Shutter : MonoBehaviour, IInteractable
{
    [field: SerializeField] public bool IsInteractable { get; set; }
    [field: SerializeField] public float InteractHoldTime { get; set; }
    [field: SerializeField] public string ObjectName { get; set; }
    [field: SerializeField] public string InteractKey { get; set; }
    [field: SerializeField] public string InteractType { get; set; }

    public void Interact()
    {
       if (IsInteractable)
       {
            IsInteractable = false;
            Destroy(EBtn);
            shutterAni.CreateTween();
       }   
    }

    public GameObject EBtn;

    [SerializeField] DOTweenAnimation shutterAni;
}