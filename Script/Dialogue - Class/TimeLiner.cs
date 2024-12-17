using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class TimeLiner : SingletonManager<TimeLiner>
{
    [SerializeField] private DialoguePresenter presenter;
    
    private TimelineElement[] generator =
        { new ScriptGenerator(), new ConversationGenerator(), new SelectGenerator(), new FavorGenerator(), new EndOfDialogueGenerator() };
    
    private SortedList<int, IDialogue> mainTimeline = new SortedList<int, IDialogue>();
    private Dictionary<int, IDialogue> subTimeline = new Dictionary<int, IDialogue>();
    private Dictionary<int, IDialogue> eventTimeline = new Dictionary<int, IDialogue>();
    private List<int> skipIndex = new List<int>();
    private List<IDialogue> log = new List<IDialogue>();
    
    private int currentIndex = 0;

    [SerializeField] private TextAsset timelineCsv;
    [SerializeField] private TextAsset eventCsv;
    
    void Awake()
    {
        Init(timelineCsv, mainTimeline);
    }

    void Init(TextAsset csv, IDictionary timeline)
    {
        SaveLoad.LoadDialogue(csv, out JArray arr);
        
        foreach (var item in arr)
        {
            int type = int.Parse(item["Type"].ToString());
            if (type is 2 or 4)
                skipIndex.Add(int.Parse(item["Index"].ToString()));
            generator[type].Element(item, timeline);
        }
    }
    
    public IDialogue ReturnData()
    {
        log.Add(mainTimeline[currentIndex]);
        return mainTimeline[currentIndex++];
    }

    public void SetSkipIndex()
    {
        int skip = 0;
        foreach (var index in skipIndex)
        {
            if (currentIndex < index)
            {
                skip = index;
                break;
            }
        }

        for (int i = currentIndex; i < skip; i++)
        {
            log.Add(mainTimeline[i]);
        }

        currentIndex = skip;
    }

    public List<IDialogue> ReturnLog()
    {
        return log;
    }
}

public enum DialogueType
{
    Init,
    Script,
    Conversation,
    Select,
    Favor
}