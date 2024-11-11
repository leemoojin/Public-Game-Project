using Newtonsoft.Json.Linq;

public class ConversationGenerator : TimelineElement
{
    public override void Element(JToken input)
    {
        Conversation conv = input.ToObject<Conversation>();
        conv.Schedule();
    }
}

public struct Conversation : ITimeline, IElement
{
    public int Index;
    public string Speaker;
    public string Context;
    
    public bool execute()
    {
        TimeLiner.SetCurrentContext(Speaker, Context);
        return true;
    }

    public void Schedule()
    {
        TimeLiner.ScheduleTimeline(Index, this);
    }
}