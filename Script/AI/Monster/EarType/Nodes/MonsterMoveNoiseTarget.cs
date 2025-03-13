using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Move Noise Target")]
public class MonsterMoveNoiseTarget : Leaf
{
    public IntReference curState;
    public FloatReference baseSpeed;
    public FloatReference runSpeedModifier;
    public BoolReference canAttack;
    public BoolReference isFocusAround;
    public BoolReference isNoiseDetect;
    public BoolReference isTargetPlayer;
    public Vector3Reference destination;// targetPos
    public TransformReference player;

    public float stopDistance = 1.5f;
    public float updateInterval = 1f;
    public Monster monster;
    private SoundData _growlsSD;
    private SoundData _screamSD;
    private float time = 0;
    private bool _isMoveFail;

    public override void OnEnter()
    {
        _isMoveFail = false;
        if (_growlsSD == null || _screamSD == null)
        {
            _growlsSD = monster.Sound.FindOtherSoundData("Growls");
            _screamSD = monster.Sound.FindOtherSoundData("Scream");
        }

        //Debug.Log($"MonsterMoveNoiseTarget - OnEnter() - isFocusAround : {isFocusAround.Value}, isDetect : {isNoiseDetect.Value}");
        if (_growlsSD.audioSource.isPlaying) monster.Sound.StopAudioSource(_growlsSD);
        if (!_screamSD.audioSource.isPlaying) monster.Sound.OtherSoundPlay(_screamSD);

        MonsterState state = (MonsterState)curState.Value;
        if (state != MonsterState.Run)
        {
            monster.Sound.StopStepAudio();
            curState.Value = (int)MonsterState.Run;
        }

        monster.Sound.PlayStepSound((MonsterState)curState.Value);
        monster.SetAnimation(false, false, true, false);

        time = 0;
        monster.agent.isStopped = false;
        monster.agent.speed = baseSpeed.Value * runSpeedModifier.Value;
        monster.agent.SetDestination(destination.Value);
    }

    public override NodeResult Execute()
    {
        if (_isMoveFail) return NodeResult.failure;
        if (canAttack.Value) return NodeResult.success;
        if (monster.Sound.GroundChange) monster.Sound.PlayStepSound((MonsterState)curState.Value);

        time += Time.deltaTime;
        if (time > updateInterval)
        {
            time = 0;
            _isMoveFail = monster.MoveToDestination(destination.Value);
        }
        if (monster.agent.pathPending) return NodeResult.running;
        if (monster.agent.hasPath) return NodeResult.running;
        if (monster.agent.remainingDistance < stopDistance)
        {
            isNoiseDetect.Value = false;
            if (!canAttack.Value && 9f < Vector3.Distance(transform.position, player.Value.position) && isTargetPlayer.Value) isFocusAround.Value = true;
            return NodeResult.success;
        }
        return NodeResult.failure;
    }
}
