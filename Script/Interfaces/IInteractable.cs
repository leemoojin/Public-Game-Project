public interface IInteractable
{
    bool IsInteractable { get; set; }
    float InteractHoldTime { get; set; } //0초 즉시 상호작용, 1초부터 홀드 상호작용


    void Interact(); // 상호작용할 오브젝트의 실행 내용

    void ActivateInteraction(); // 메세지 출력
}