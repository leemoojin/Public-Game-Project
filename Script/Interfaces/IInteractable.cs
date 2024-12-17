public interface IInteractable
{
    bool IsInteractable { get; set; }
    float InteractHoldTime { get; set; } //0초 즉시 상호작용, 1초부터 홀드 상호작용
    string ObjectName { get; set; }// SO
    string InteractKey { get; set; }// SO
    string InteractType { get; set; }// SO

    void Interact(); // 상호작용할 오브젝트의 실행 내용

    //void ActivateInteraction(); // ui 메세지 출력
}