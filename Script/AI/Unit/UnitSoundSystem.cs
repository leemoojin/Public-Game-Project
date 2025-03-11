using NPC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static UnitEnum;

public class UnitSoundSystem : MonoBehaviour
{
    [field: SerializeField] public UnitType UnitType { get; private set; }
    [field: SerializeField] public Grounds CurGround { get; private set; }

    public List<SoundData> stepSoundList;
    public List<SoundData> otherSoundList;
    public AudioSource stepAS;
    public AudioSource otherAS;

    public bool GroundChange { get; private set; }

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

        if (other.tag == "Concrete")
        {
            if (CurGround != Grounds.Concrete) GroundChange = true;
            CurGround = Grounds.Concrete;
        }
        if (other.tag == "Wet")
        {
            if (CurGround != Grounds.Wet) GroundChange = true;
            CurGround = Grounds.Wet;
        }
    }

    public void PlayStepSound(NPCState curstate)
    {
        if (stepAS.isPlaying && !GroundChange) return;
        if (CurGround == Grounds.Untagged) return;

        GroundChange = false;
        SoundData tempData = null;
        
        if ((curstate & NPCState.Run) == NPCState.Run)
        {
            if (CurGround == Grounds.Concrete) tempData = FindSoundData("ConcreteRun");
            else if (CurGround == Grounds.Wet) tempData = FindSoundData("WetRun");
        }
        else if ((curstate & NPCState.Crouch) == NPCState.Crouch)
        {
            if (CurGround == Grounds.Concrete) tempData = FindSoundData("ConcreteCrouch");
            else if (CurGround == Grounds.Wet) tempData = FindSoundData("WetCrouch");
        }
        else if ((curstate & NPCState.Move) == NPCState.Move)
        {
            if (CurGround == Grounds.Concrete) tempData = FindSoundData("ConcreteWalk");
            else if (CurGround == Grounds.Wet) tempData = FindSoundData("WetWalk");
        }

        stepAS.loop = true;
        stepAS.clip = tempData.noises[0];
        stepAS.volume = tempData.volume + (Random.Range(-0.1f, 0.1f));
        stepAS.pitch = tempData.pitch + (Random.Range(-0.1f, 0.1f));
        stepAS.Play();
    }

    public void PlaySoundTemp()
    {
        SoundData tempData = null;
        tempData = FindSoundData("ConcreteWalk");
        stepAS.loop = true;
        stepAS.clip = tempData.noises[0];
        stepAS.volume = tempData.volume + (Random.Range(-0.1f, 0.1f));
        stepAS.pitch = tempData.pitch + (Random.Range(-0.1f, 0.1f));
        stepAS.Play();
    }

    public void OtherSoundPlay(string tag)
    {
        SoundData tempData = null;
        tempData = FindOtherSoundData(tag);
        otherAS.clip = tempData.noises[0];
        otherAS.Play();
    }

    private SoundData FindSoundData(string tag)
    {
        SoundData tempData = null;

        for (int i = 0; i < stepSoundList.Count; i++)
        {
            if (stepSoundList[i].tag == tag) tempData = stepSoundList[i];
        }

        if (tempData == null) Debug.LogError("UnitSoundSystem - none data");

        return tempData;
    }

    private SoundData FindOtherSoundData(string tag)
    {
        SoundData tempData = null;

        for (int i = 0; i < otherSoundList.Count; i++)
        {
            if (otherSoundList[i].tag == tag) tempData = otherSoundList[i];
        }

        if (tempData == null) Debug.LogError("UnitSoundSystem - none data");

        return tempData;
    }

    public void StopStepAudio()
    {
        if (stepAS.isPlaying)
        {
            stepAS.Stop(); 
        }
    }
}
