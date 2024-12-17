using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePresenter : MonoBehaviour
{
    [SerializeField] private TimeLiner timeLiner;
    [SerializeField] private DialogueView view;

    private Coroutine auto;

    public void RequestData()
    {
        var data = timeLiner.ReturnData();
        data.Get(view);
    }

    public void RequestSkip()
    {
        timeLiner.SetSkipIndex();
    }

    public void RequestLog()
    {
        
    }

    public void SaveLog()
    {
        
    }

    private IEnumerator autoPlay(bool isAuto)
    {
        float time = 0;

        if (isAuto)
        {
            while (time < 3f)
            {
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
