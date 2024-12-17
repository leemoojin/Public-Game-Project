using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueView : MonoBehaviour
{
    [SerializeField] private DialoguePresenter presenter;

    [SerializeField] private GameObject UI_2D;
    [SerializeField] private Text Speaker;
    [SerializeField] private Text Context;
    [SerializeField] private GameObject SelectPanel;
    [SerializeField] private GameObject SelectPrefab;
    [SerializeField] private GameObject LogPanel;
    [SerializeField] private GameObject LogViewPort;
    [SerializeField] private GameObject LogPrefab;
    [SerializeField] private List<GameObject> Logs;
    
    private DialogueType currentType = DialogueType.Init;
    private bool dotextEnd = true;
    private bool auto = true;
    private bool autoPlaying = false;
    private TweenerCore<string, string, StringOptions> tween;
    private string selectLog;

    private Coroutine autoCoroutine;
    private WaitForSeconds interval = new WaitForSeconds(3.0f);

    private void OnEnable()
    {
        SetType(DialogueType.Conversation);
        presenter.RequestData();
    }

    public void OpenDialogue(bool is2D, bool _auto)
    {
        gameObject.SetActive(true);
        UI_2D.gameObject.SetActive(is2D);
        auto = _auto;
    }

    public void CloseDialogue()
    {
        gameObject.SetActive(false);
    }
    
    public void OnClick(InputAction.CallbackContext value)
    {
        if (UIClicked())
        {
            Debug.Log("UI Click");
            return;
        }
        
        if (dotextEnd)
        {
            if (autoPlaying)
            {
                StopCoroutine(autoCoroutine);
                autoPlaying = false;
            }
            
            presenter.RequestData();
        }
        else
        {
            tween.Complete();
        }
    }

    public void SkipButtonClick()
    {
        if (autoPlaying)
        {
            StopCoroutine(autoCoroutine);
            autoPlaying = false;
        }
        
        presenter.RequestSkip();
        presenter.RequestData();
    }

    public void LogButtonClick()
    {
        LogPanel.SetActive(!LogPanel.activeInHierarchy);
        if (LogPanel.activeInHierarchy)
        {
            presenter.RequestLog();
        }
        else
        {
            foreach (var log in Logs)
            {
                Destroy(log);
            }
        }
    }

    public void CreateLogElement(string _log)
    {
        GameObject go = Instantiate(LogPrefab, LogViewPort.transform);
        Logs.Add(go);
        go.GetComponent<Text>().text = _log;
    }

    public void SetContext(string _speaker, string _context)
    {
        SetType(DialogueType.Conversation);
        Speaker.text = _speaker;
        PlayDoText(_context);
    }

    public void SetScript(string _context)
    {
        SetType(DialogueType.Script);
        PlayDoText(_context);
    }

    public void SetSelects(string[] _selects)
    {
        dotextEnd = false;
        SetType(DialogueType.Select);
        
        for (int i = 0; i < _selects.Length; i++)
        {
            GameObject go = Instantiate(SelectPrefab, SelectPanel.transform);
            
            Text selectText = go.GetComponentInChildren<Text>();
            Button selectButton = go.GetComponentInChildren<Button>();
            
            //선택지 버튼 클릭시 카르마 변경하는 event 추가 필요
            selectButton.onClick.AddListener(presenter.RequestData);
            
            selectText.text = _selects[i];
        }
    }

    private void PlayDoText(string _context)
    {
        Context.text = null;
        dotextEnd = false;
        tween = Context.DOText(_context, _context.Length * 0.05f).SetEase(Ease.Linear);
        tween.OnComplete(() =>
        {
            dotextEnd = true;
            if (auto)
            {
                autoCoroutine = StartCoroutine(autoPlay());
            }
        });
    }

    private void SetType(DialogueType type)
    {
        if (currentType != type)
        {
            currentType = type;
            switch (type)
            {
                case DialogueType.Script :
                    UiActive(false, true, false);
                    break;
                case DialogueType.Conversation :
                    UiActive(true, true, false);
                    break;
                case DialogueType.Select :
                    UiActive(false,false,true);
                    break;
            }
        }
    }

    private void UiActive(bool speaker, bool context, bool select)
    {
        Speaker.transform.parent.gameObject.SetActive(speaker);
        Context.gameObject.SetActive(context);
        SelectPanel.gameObject.SetActive(select);
    }

    private IEnumerator autoPlay()
    {
        autoPlaying = true;

        yield return interval;

        Context.text = null;
        presenter.RequestData();
        autoPlaying = false;
    }

    private bool UIClicked()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }

        return false;
    }
}
