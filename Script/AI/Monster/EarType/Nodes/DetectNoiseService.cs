using MBT;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Detect Noise Service")]
public class DetectNoiseService : Service
{
    public BoolReference variableToSetBool;// isNoiseDetect
    public BoolReference isFocusAround;
    public BoolReference canAttack;
    public Vector3Reference targetPos;// detination
    public FloatReference curDetectNoise;// check board
    public TransformReference self;
    public IntReference curState;
    public FloatReference detectRangeMax;
    public FloatReference detectRangeMin;
    public FloatReference detectNoiseMin;
    public FloatReference detectNoiseMax;

    public LayerMask targetMask;
    public GameObject biggestNoiseObj;
    public List<Collider> noiseMakers = new List<Collider>(80);
    public Monster monster;

    private SoundData _growlsSD;
    private bool _isFindPlayer;
    private Collider[] _detectColliders = new Collider[80];
    private const int MaxIterations = 5;

    public override void OnEnter()
    {
        if (_growlsSD == null) _growlsSD = monster.Sound.FindOtherSoundData("Growls");
        base.OnEnter();
    }

    public override void Task()
    {
        if (canAttack.Value) return;
        Detect();
    }

    private void Detect()
    {
        noiseMakers.Clear();
        biggestNoiseObj = null;
        curDetectNoise.Value = 0f;
        _isFindPlayer = false;

        int count = 0;
        int iteration = 0;

        while (iteration < MaxIterations)
        {
            count = Physics.OverlapSphereNonAlloc(self.Value.position, detectRangeMax.Value, _detectColliders, targetMask);
            if (count < _detectColliders.Length) break;
            _detectColliders = new Collider[_detectColliders.Length * 2];
            noiseMakers = new List<Collider>(_detectColliders.Length * 2);
            iteration++;
        }

        for (int i = 0; i < count; i++)
        {
            noiseMakers.Add(_detectColliders[i]);
            if (_detectColliders[i].gameObject.layer == LayerMask.NameToLayer("Player")) _isFindPlayer = true;
            CompareNoise(_detectColliders[i].gameObject);
        }

        UpdateMonsterSound();
        if (biggestNoiseObj == null) return;
        CheckNoiseAmount(biggestNoiseObj);
    }

    private void UpdateMonsterSound()
    {
        if (_isFindPlayer)
        {
            if (!_growlsSD.audioSource.isPlaying) monster.Sound.OtherSoundPlay(_growlsSD);
            else _growlsSD.audioSource.volume = 1f;
        }
        else if (!_isFindPlayer && _growlsSD.audioSource.isPlaying) monster.Sound.StopAudioSource(_growlsSD);
    }

    private void CheckNoiseAmount(GameObject noiseMaker)
    {
        float distance = Vector3.Distance(self.Value.position, noiseMaker.transform.position);

        if (distance <= detectRangeMax.Value && curDetectNoise.Value >= detectNoiseMax.Value)
        {
            if (curState.Value == (int)MonsterState.Lost) isFocusAround.Value = false;
            variableToSetBool.Value = true;
            targetPos.Value = biggestNoiseObj.transform.position;
            return;
        }

        if (distance <= detectRangeMin.Value && curDetectNoise.Value >= detectNoiseMin.Value)
        {
            if (curState.Value == (int)MonsterState.Lost) isFocusAround.Value = false;
            variableToSetBool.Value = true;
            targetPos.Value = biggestNoiseObj.transform.position;
            return;
        }
    }

    private void CompareNoise(GameObject noiseMaker)
    {
        if (noiseMaker.TryGetComponent<INoise>(out INoise noiseComponent))
        {
            float noiseAmount = noiseComponent.CurNoiseAmount;

            if (curDetectNoise.Value < noiseAmount)
            {
                curDetectNoise.Value = noiseAmount;
                biggestNoiseObj = noiseMaker;
            }
        }
    }
}
