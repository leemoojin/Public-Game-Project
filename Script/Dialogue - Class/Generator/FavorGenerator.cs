using System.Collections;
using Newtonsoft.Json.Linq;

public class FavorGenerator : TimelineElement
{
    public override void Element(JToken input, IDictionary timeline)
    {
        Favor favor = input.ToObject<Favor>();
        string[] contextInput = input["Context"].ToString().Split("/");
        favor.Like = contextInput[0];
        favor.Hate = contextInput[1];
        
        timeline.Add(favor.Index, favor);
    }
}

public struct Favor : IDialogue
{
    public int Index;
    public string Speaker;
    public string Like;
    public string Hate;

    public void Get(out string data)
    {
        throw new System.NotImplementedException();
    }

    public void Get(DialogueView view)
    {
        throw new System.NotImplementedException();
    }
}