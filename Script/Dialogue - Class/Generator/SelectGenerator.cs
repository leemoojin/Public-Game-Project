using System.Collections;
using Newtonsoft.Json.Linq;

public class SelectGenerator : TimelineElement
{
    public override void Element(JToken input, IDictionary timeline)
    {
        Select select = input.ToObject<Select>();
        select.Selectcs = input["Select"].ToString().Split("/");
        select.Karmas = new int[select.Selectcs.Length];
        string[] karmaString = input["Karma"].ToString().Split("/");
        
        for (int i = 0; i < select.Karmas.Length; i++)
            select.Karmas[i] = int.Parse(karmaString[i]);
        
        timeline.Add(select.Index, select);
    }
}

public struct Select : IDialogue
{
    public int Index;
    public string[] Selectcs;
    public int[] Karmas;

    public void Get(DialogueView view)
    {
        view.SetSelects(Selectcs);
    }
}