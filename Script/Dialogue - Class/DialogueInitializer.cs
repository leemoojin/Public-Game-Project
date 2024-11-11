using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class DialogueInitializer : MonoBehaviour
{
    [SerializeField] private TextAsset timelineCsv;

    [SerializeField] private TextAsset conversationCsv;
    [SerializeField] private TextAsset scriptCsv;
    [SerializeField] private TextAsset favorCsv;

    private string conversationJson;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveLoad.LoadDialogue(timelineCsv);
        //Init<Script>(SaveLoad.LoadDialogue<Script[]>(scriptCsv));
        //Init<Conversation>(SaveLoad.LoadDialogue<Conversation[]>(conversationCsv));
    }

    private void Init<T>(T[] items) where T : IElement
    {
        foreach (var item in items)
        {
            item.Schedule();
        }
    }
}
