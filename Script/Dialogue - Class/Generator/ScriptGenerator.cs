using Newtonsoft.Json.Linq;

public class ScriptGenerator : TimelineElement
{
    public override void Element(JToken input)
    {
        Script script = input.ToObject<Script>();
        script.Schedule();
    }
}

public struct Script : ITimeline, IElement
{
    public int Index;
    public string Context;

    public bool execute()
    {
        TimeLiner.SetCurrentContext("", Context);
        return true;
    }

    public void Schedule()
    {
        TimeLiner.ScheduleTimeline(Index, this);
    }
}