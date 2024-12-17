using System.Collections;
using Newtonsoft.Json.Linq;

public abstract class TimelineElement
{
    public abstract void Element(JToken input, IDictionary timeline);
}
