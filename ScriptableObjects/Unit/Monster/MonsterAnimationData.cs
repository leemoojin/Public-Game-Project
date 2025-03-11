using UnityEngine;

[CreateAssetMenu(fileName = "MonsterAnimationData", menuName = "SO/Unit/Monster/AnimationData")]
public class MonsterAnimationData : ScriptableObject
{
    private string _idleParameterName = "Idle";
    private string _walkParameterName = "Walk";
    private string _runParameterName = "Run";
    private string _attackParameterName = "Attack";
    private string _lostTargetParameterName = "Lost";
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int LostTargetParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        WalkParameterHash = Animator.StringToHash(_walkParameterName);
        RunParameterHash = Animator.StringToHash(_runParameterName);
        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        LostTargetParameterHash = Animator.StringToHash(_lostTargetParameterName);
    }
}
