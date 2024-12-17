using MBT;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Detect Noise Service")]
public class DetectNoiseService : Service
{
    //public TransformReference variableToSet = new TransformReference(VarRefMode.DisableConstant);
    public BoolReference variableToSetBool = new BoolReference(VarRefMode.DisableConstant);// isNoiseDetect
    public BoolReference isFocusAround = new BoolReference(VarRefMode.DisableConstant);
    public Vector3Reference targetPos;// detination
    public FloatReference curDetectNoise;// check board
    public TransformReference self;
    public IntReference curState;

    // SO
    public LayerMask targetMask = -1;// SO
    public float detectRange = 60f;// SO
    public float detectNoiseMax = 9f;// SO
    public float detectNoiseMin = 3;// SO

    public GameObject biggestNoiseObj;// private
    public List<Collider> noiseMakers = new List<Collider>();

    public override void Task()
    {
        Detect();
    }

    private void Detect()
    {
        //Debug.Log("DetectNoiseService - Task()");
        noiseMakers.Clear();
        biggestNoiseObj = null;
        curDetectNoise.Value = 0f;

        Collider[] colliders = Physics.OverlapSphere(self.Value.position, detectRange, targetMask);

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
        if (Vector3.Distance(self.Value.position, noiseMaker.transform.position) <= detectRange && curDetectNoise.Value >= detectNoiseMax)
        {
            if (curState.Value == (int)MonsterState.FocusAround) isFocusAround.Value = false;

            variableToSetBool.Value = true;
            targetPos.Value = biggestNoiseObj.transform.position;
            return;
        }

        if (Vector3.Distance(self.Value.position, noiseMaker.transform.position) <= detectRange * 0.5f && curDetectNoise.Value >= detectNoiseMin)
        {
            if (curState.Value == (int)MonsterState.FocusAround) isFocusAround.Value = false;

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
}
