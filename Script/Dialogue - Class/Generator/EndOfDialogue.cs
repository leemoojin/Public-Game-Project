using System.Collections;
using Newtonsoft.Json.Linq;

public class EndOfDialogueGenerator : TimelineElement
{
    public override void Element(JToken input, IDictionary timeline)
    {
        EndOfDialogue end = input.ToObject<EndOfDialogue>();
        
        timeline.Add(end.index, end);
    }
}

public class EndOfDialogue : IDialogue
{
    public int index;
    
    public void Get(DialogueView view)
    {
        view.CloseDialogue();
    }
}
