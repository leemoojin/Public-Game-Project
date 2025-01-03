using MBT;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EyeTypeHand : MonoBehaviour
{
    public Blackboard BB;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 3)
        {
            //game over
            BB.GetVariable<Variable<bool>>("isWork").Value = false;
            return;
        }
        else if (other.gameObject.layer == 7) 
        {
            //Debug.Log($"");
            //game over check
            BB.GetVariable<Variable<bool>>("isWork").Value = true;
            return;
        }
        
        //fail, rework
        BB.GetVariable<Variable<bool>>("isWork").Value = true;
    }
}
