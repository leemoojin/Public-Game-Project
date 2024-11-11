using MBT;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Detect Noise Service")]
public class DetectNoiseService : Service
{

    public TransformReference variableToSet;
    //public TransformReference variableToSet = new TransformReference(VarRefMode.DisableConstant);


    public LayerMask targetMask = -1;
    public float detectRange = 60f;
    public float detectNoiseMax = 9f;
    public float detectNoiseMin = 3;
    public TransformReference self;
    public List<Collider> noiseMakers = new List<Collider>();

    public GameObject biggestNoiseObj;
    public Vector3Reference targetPos;
    public FloatReference curDetectNoise;
    //public float beforeNoise = 0f;


    public override void Task()
    {
        Detect();
    }

    private void Detect()
    {
        //Debug.Log("DetectNoiseService - Task()");
        noiseMakers.Clear();
        biggestNoiseObj = null;
        //variableToSet.Value = null;
        //targetPos.Value = Vector3.zero;

        if (curDetectNoise.Value == 0f) variableToSet.Value = null;
        if (variableToSet.Value == null) curDetectNoise.Value = 0f;

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
            variableToSet.Value = noiseMaker.transform;
            targetPos.Value = biggestNoiseObj.transform.position;
            return;
        }

        if (Vector3.Distance(self.Value.position, noiseMaker.transform.position) <= detectRange * 0.5f && curDetectNoise.Value >= detectNoiseMin)
        {
            variableToSet.Value = noiseMaker.transform;
            targetPos.Value = biggestNoiseObj.transform.position;
            return;
        }
    }

    private bool CompareNoise(GameObject noiseMaker)
    {
        float noiseAmount = noiseMaker.GetComponent<INoise>().CurNoiseAmount;

        if (curDetectNoise.Value < noiseAmount)
        {
            //beforeNoise = curDetectNoise.Value;
            curDetectNoise.Value = noiseAmount;
            biggestNoiseObj = noiseMaker;
            return true;
        }
        return false;
    }
}
