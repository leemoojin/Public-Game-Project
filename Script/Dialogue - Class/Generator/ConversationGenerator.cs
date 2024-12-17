using System.Collections;
using System.Text;
using Newtonsoft.Json.Linq;

public class ConversationGenerator : TimelineElement
{
    public override void Element(JToken input, IDictionary timeline)
    {
        Conversation conv = input.ToObject<Conversation>();
        
        timeline.Add(conv.Index, conv);
    }
}

public struct Conversation : IDialogue
{
    public int Index;
    public string Speaker;
    public string Context;
    
    public void Get(out string data)
    {
        data = Context;
    }

    public void LoadLog(out string data)
    {
        data = $"{Speaker} : {Context}";
    }

    public void Get(DialogueView view)
    {
        view.SetContext(Speaker, Context);
    }
}