using System.Collections;
using Newtonsoft.Json.Linq;

public class ScriptGenerator : TimelineElement
{
    public override void Element(JToken input, IDictionary timeline)
    {
        Script script = input.ToObject<Script>();
        
        timeline.Add(script.Index, script);
    }
}

public struct Script : IDialogue
{
    public int Index;
    public string Context;

    public bool execute(DialogueView view)
    {
        view.SetScript(Context);
        return true;
    }

    public void LoadLog(out string data)
    {
        data = Context;
    }

    public void Get(DialogueView view)
    {
        view.SetScript(Context);
    }
}