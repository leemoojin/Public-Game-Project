using System.Collections.Generic;
using UnityEngine;

public enum Grounds
{
    Untagged,
    Concrete,
    Wet
}

public class FootstepsSystem : MonoBehaviour
{    
    public Player Player { get; private set; }
    public List<PlayerSoundData> stepSoundList;

    [field: SerializeField] public Grounds CurGround { get; private set; }
    public Transform step;

    private AudioSource stepAS;


    private void Awake()
    {
        Player = GetComponent<Player>();        
    }

    private void Start()
    {
        stepAS = step.GetComponent<AudioSource>();
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Untagged") return;

        if (hit.collider.tag == "Concrete") CurGround = Grounds.Concrete;
        if (hit.collider.tag == "Wet") CurGround = Grounds.Wet;
    }

    public void PlayStepSound(States curstate)
    {
        if (stepAS.isPlaying) return;
        if (CurGround == Grounds.Untagged) return;

        PlayerSoundData tempData = null;
        if (curstate == States.WalkState)
        {
            if (CurGround == Grounds.Concrete) tempData = FindSoundData("ConcreteWalk");
            else if (CurGround == Grounds.Wet) tempData = FindSoundData("WetWalk");
        }
        else if (curstate == States.RunState)
        {
            if (CurGround == Grounds.Concrete) tempData = FindSoundData("ConcreteRun");
            else if (CurGround == Grounds.Wet) tempData = FindSoundData("WetRun");
        }
        else if (curstate == States.CrouchState)
        {
            if (CurGround == Grounds.Concrete) tempData = FindSoundData("ConcreteCrouch");
            else if (CurGround == Grounds.Wet) tempData = FindSoundData("WetCrouch");
        }

        stepAS.clip = tempData.noises[0];
        stepAS.volume = tempData.volume + (Random.Range(-0.1f, 0.1f));
        stepAS.pitch = tempData.pitch + (Random.Range(-0.1f, 0.1f));
        stepAS.Play();
        Player.CurNoiseAmount += tempData.noiseAmount;
        if (Player.CurNoiseAmount >= Player.SumNoiseAmount) Player.CurNoiseAmount = Player.SumNoiseAmount;
    }

    private PlayerSoundData FindSoundData(string tag)
    {
        PlayerSoundData tempData = null;

        for (int i = 0; i < stepSoundList.Count; i++)
        {
            if (stepSoundList[i].tag == tag) tempData = stepSoundList[i];
        }

        if (tempData == null) Debug.LogError("FootstepsSystem - none data");

        return tempData;
    }

}
