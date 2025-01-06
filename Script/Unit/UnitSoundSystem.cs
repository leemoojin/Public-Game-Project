using NPC;
using System.Collections.Generic;
using UnityEngine;
using static UnitEnum;

public class UnitSoundSystem : MonoBehaviour
{
    [field: SerializeField] public UnitType UnitType { get; private set; }
    [field: SerializeField] public Grounds CurGround { get; private set; }

    public List<SoundData> soundList;
    public AudioSource stepAS;
    public AudioSource otherAS;


    private void Awake()
    {
        if (gameObject.layer == 7)
        {
            UnitType = UnitType.Npc;
        }
        else if (gameObject.layer == 8)
        {
            UnitType = UnitType.EyeTypeMonster;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Untagged") return;

        if (other.tag == "Concrete") CurGround = Grounds.Concrete;
        if (other.tag == "Wet") CurGround = Grounds.Wet;

    }

    public void PlayStepSound(NPCState curstate)
    {
        if (stepAS.isPlaying) return;
        if (CurGround == Grounds.Untagged) return;

        SoundData tempData = null;
        if ((curstate & NPCState.Move) == NPCState.Move)
        {
            if (CurGround == Grounds.Concrete) tempData = FindSoundData("ConcreteWalk");
            else if (CurGround == Grounds.Wet) tempData = FindSoundData("WetWalk");
        }
        else if ((curstate & NPCState.Run) == NPCState.Run)
        {
            if (CurGround == Grounds.Concrete) tempData = FindSoundData("ConcreteRun");
            else if (CurGround == Grounds.Wet) tempData = FindSoundData("WetRun");
        }
        else if ((curstate & NPCState.Crouch) == NPCState.Crouch)
        {
            if (CurGround == Grounds.Concrete) tempData = FindSoundData("ConcreteCrouch");
            else if (CurGround == Grounds.Wet) tempData = FindSoundData("WetCrouch");
        }

        stepAS.clip = tempData.noises[0];
        stepAS.volume = tempData.volume + (Random.Range(-0.1f, 0.1f));
        stepAS.pitch = tempData.pitch + (Random.Range(-0.1f, 0.1f));
        stepAS.Play();
        //Debug.Log(tempData.noiseAmount);
    }

    private SoundData FindSoundData(string tag)
    {
        SoundData tempData = null;

        for (int i = 0; i < soundList.Count; i++)
        {
            if (soundList[i].tag == tag) tempData = soundList[i];
        }

        if (tempData == null) Debug.LogError("FootstepsSystem - none data");

        return tempData;
    }
}
