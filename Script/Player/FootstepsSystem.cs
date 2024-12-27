using System.Collections.Generic;
using UnityEngine;
using static PlayerEnum;

public class FootstepsSystem : MonoBehaviour
{
    [field: SerializeField] public GameObject Unit { get; private set; }
    [field: SerializeField] public Grounds CurGround { get; private set; }
    public Player Player { get; private set; }

    public List<SoundData> stepSoundList;
    public AudioSource stepAS;

    private void Awake()
    {
        if (LayerMask.LayerToName(Unit.layer) == "Player")
        {
            Player = Unit.GetComponent<Player>();
        }
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Untagged") return;

        if (hit.collider.tag == "Concrete") CurGround = Grounds.Concrete;
        if (hit.collider.tag == "Wet") CurGround = Grounds.Wet;
    }

    public void PlayStepSound(PlayerState curstate)
    {
        if (stepAS.isPlaying) return;
        if (CurGround == Grounds.Untagged) return;

        SoundData tempData = null;
        if (curstate == PlayerState.WalkState)
        {
            if (CurGround == Grounds.Concrete) tempData = FindSoundData("ConcreteWalk");
            else if (CurGround == Grounds.Wet) tempData = FindSoundData("WetWalk");
        }
        else if (curstate == PlayerState.RunState)
        {
            if (CurGround == Grounds.Concrete) tempData = FindSoundData("ConcreteRun");
            else if (CurGround == Grounds.Wet) tempData = FindSoundData("WetRun");
        }
        else if (curstate == PlayerState.CrouchState)
        {
            if (CurGround == Grounds.Concrete) tempData = FindSoundData("ConcreteCrouch");
            else if (CurGround == Grounds.Wet) tempData = FindSoundData("WetCrouch");
        }

        stepAS.clip = tempData.noises[0];
        stepAS.volume = tempData.volume + (Random.Range(-0.1f, 0.1f));
        stepAS.pitch = tempData.pitch + (Random.Range(-0.1f, 0.1f));
        stepAS.Play();
        Debug.Log(tempData.noiseAmount);
        Player.CurNoiseAmount += tempData.noiseAmount;
        if (Player.CurNoiseAmount >= Player.SumNoiseAmount) Player.CurNoiseAmount = Player.SumNoiseAmount;
    }

    private SoundData FindSoundData(string tag)
    {
        SoundData tempData = null;

        for (int i = 0; i < stepSoundList.Count; i++)
        {
            if (stepSoundList[i].tag == tag) tempData = stepSoundList[i];
        }

        if (tempData == null) Debug.LogError("FootstepsSystem - none data");

        return tempData;
    }

}
