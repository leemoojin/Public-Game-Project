using MBT;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Detect Noise Service")]
public class DetectNoiseService : Service
{
    //public TransformReference variableToSet = new TransformReference(VarRefMode.DisableConstant);
    public BoolReference variableToSetBool;// isNoiseDetect
    public BoolReference isFocusAround;
    public BoolReference canAttack;
    public Vector3Reference targetPos;// detination
    public FloatReference curDetectNoise;// check board
    public TransformReference self;
    public IntReference curState;
    public FloatReference detectRange;
    public FloatReference detectNoiseMin;
    public FloatReference detectNoiseMax;

    // SO
    public LayerMask targetMask = -1;
    //public float detectRange = 60f;// SO
    //public float detectNoiseMax = 9f;// SO
    //public float detectNoiseMin = 3;// SO

    public GameObject biggestNoiseObj;// private
    public List<Collider> noiseMakers = new List<Collider>();

    public override void OnEnter()
    {
        base.OnEnter();
        //Debug.Log($"DetectNoiseService - OnEnter() - isFocusAround : {isFocusAround.Value}");

    }

    public override void Task()
    {
        if (canAttack.Value) return;
        Detect();
    }

    private void Detect()
    {
        //Debug.Log("DetectNoiseService - Task()");
        noiseMakers.Clear();
        biggestNoiseObj = null;
        curDetectNoise.Value = 0f;

        Collider[] colliders = Physics.OverlapSphere(self.Value.position, detectRange.Value, targetMask);

        foreach (Collider col in colliders)
        {
            if (col.tag == "NoiseMaker" || col.tag == "Player")
            {
                noiseMakers.Add(col);
                CompareNoise(col.gameObject);
            }
        }

        if (biggestNoiseObj == null) return;

        // check noiseAmount
        CheckNoiseAmount(biggestNoiseObj);
    }


    private void CheckNoiseAmount(GameObject noiseMaker)
    {       
        if (Vector3.Distance(self.Value.position, noiseMaker.transform.position) <= detectRange.Value && curDetectNoise.Value >= detectNoiseMax.Value)
        {
            if (curState.Value == (int)EarTypeMonsterState_.FocusAround) isFocusAround.Value = false;

            variableToSetBool.Value = true;
            targetPos.Value = biggestNoiseObj.transform.position;
            return;
        }

        if (Vector3.Distance(self.Value.position, noiseMaker.transform.position) <= detectRange.Value * 0.5f && curDetectNoise.Value >= detectNoiseMin.Value)
        {
            if (curState.Value == (int)EarTypeMonsterState_.FocusAround) isFocusAround.Value = false;

            variableToSetBool.Value = true;
            targetPos.Value = biggestNoiseObj.transform.position;
            return;
        }
    }

    private void CompareNoise(GameObject noiseMaker)
    {
        float noiseAmount = noiseMaker.GetComponent<INoise>().CurNoiseAmount;

        if (curDetectNoise.Value < noiseAmount)
        {
            curDetectNoise.Value = noiseAmount;
            biggestNoiseObj = noiseMaker;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        //Debug.Log($"DetectNoiseService - OnExit() - isFocusAround : {isFocusAround.Value}");

    }
}
