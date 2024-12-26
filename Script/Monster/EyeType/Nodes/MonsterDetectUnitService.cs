using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Example/Monster Detect Unit Service")]
public class MonsterDetectUnitService : Service
{

    public BoolReference variableToSet;// isUnitDetect
    public TransformReference self;
    public TransformReference detectorHigh;
    public TransformReference detectorLow;
    public TransformReference target;// Unit

    public LayerMask obstructionMask;
    public LayerMask targetMask;
    private bool _isLookObstruct = false;


    public override void Task()
    {
        throw new System.NotImplementedException();
    }

    private void Detect()
    {
        _isLookObstruct = false;

     


        if (_isLookObstruct) variableToSet.Value = false;
    }

}
