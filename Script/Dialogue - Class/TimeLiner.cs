using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TimeLiner : MonoBehaviour
{
    private static SortedList<int, ITimeline> timeline = new SortedList<int, ITimeline>();
    private static CurrentContext current = new CurrentContext();
    private int currentIndex = 0;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (timeline[currentIndex].execute())
            {
                currentIndex += 1;
            }
            if (!current.SelectOn)
                TestUI.Instance.SetContext(current.Speaker, current.Context);
            else
            {
                StringBuilder test = new StringBuilder();
                
                for (int i = 0; i < current.Select.Length; i++)
                {
                    test.Append($"{i} : {current.Select[i]} ({current.Karma[i]})\n");
                }
                
                TestUI.Instance.SetSelect(test.ToString());
            }
        }
    }

    private IEnumerator ExecuteTimeline()
    {
        yield return null;
    }

    public static void ScheduleTimeline(int index, ITimeline input)
    {
        timeline.Add(index, input);
    }

    public static void SetCurrentContext(string speaker, string context)
    {
        current.Speaker = speaker;
        current.Context = context;
        current.SelectOn = false;
    }

    public static void SetSelect(string[] select, int[] karma)
    {
        current.Select = select;
        current.Karma = karma;
        current.SelectOn = true;
    }
}

public struct CurrentContext
{
    public string Speaker;
    public string Context;
    public bool SelectOn;
    public string[] Select;
    public int[] Karma;
}