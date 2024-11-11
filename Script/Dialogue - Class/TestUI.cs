using System;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    [SerializeField] private Text speaker;
    [SerializeField] private Text context;
    [SerializeField] private Text select;

    public static TestUI Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetContext(string _speaker, string _context)
    {
        speaker.text = _speaker;
        context.text = _context;
    }

    public void SetSelect(string _select)
    {
        select.text = _select;
    }
}
