using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class SelectGenerator : TimelineElement
{
    public override void Element(JToken input)
    {
        Select select = input.ToObject<Select>();
        select.Selectcs = input["Select"].ToString().Split("/");
        select.Karmas = new int[select.Selectcs.Length];
        string[] karmaString = input["Karma"].ToString().Split("/");
        
        for (int i = 0; i < select.Karmas.Length; i++)
            select.Karmas[i] = int.Parse(karmaString[i]);
        
        select.Schedule();
    }
}

public struct Select : IElement, ITimeline
{
    public int Index;
    public string[] Selectcs;
    public int[] Karmas;

    public bool execute()
    {
        TimeLiner.SetSelect(Selectcs, Karmas);
        return true;
    }

    public void Schedule()
    {
        TimeLiner.ScheduleTimeline(Index, this);
    }
}