using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/EarType Monster Attack")]
public class EarTypeMonsterAttack : Leaf
{
    public IntReference curState;
    public BoolReference isWork;
    public BoolReference isNoiseDetect;
    public BoolReference isTargetPlayer;
    public TransformReference player;
    public TransformReference npc;

    public Monster monster;
    private SoundData _screamSD;
    private SoundData _growlsSD;

    public override void OnEnter()
    {
        if (_screamSD == null || _growlsSD == null)
        {
            _screamSD = monster.Sound.FindOtherSoundData("Scream");
            _growlsSD = monster.Sound.FindOtherSoundData("Growls");
        }
        if (_screamSD.audioSource.isPlaying) monster.Sound.StopAudioSource(_screamSD);
        if (_growlsSD.audioSource.isPlaying) monster.Sound.StopAudioSource(_growlsSD);
        monster.Sound.StopStepAudio();

        if (isTargetPlayer.Value)
        {
            //if (player.Value.gameObject.TryGetComponent<IAttackable>(out attackable)) attackable.OnHitSuccess(UnitType.EarTypeMonster);
            GameManager.Instance.Player.OnHitSuccess(UnitEnum.UnitType.EarTypeMonster);
            Debug.Log($"EarTypeMonsterAttack - OnEnter() - ÇÃ·¹ÀÌ¾î »ç¸Á - gameOver");
        }
        else
        {
            if (npc.Value.gameObject.TryGetComponent<IAttackable>(out IAttackable attackable)) attackable.OnHitSuccess();
            Debug.Log($"EarTypeMonsterAttack - OnEnter() - npc »ç¸Á  - gameOver");
        }

        curState.Value = (int)MonsterState.Attack;
        monster.SetAnimation(true, false, false, false);

        isWork.Value = false;
        isNoiseDetect.Value = false;
    }

    public override NodeResult Execute()
    {
        return NodeResult.success;
    }
}
