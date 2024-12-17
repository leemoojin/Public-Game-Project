using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneEnum
{
    MainMenuScene,
    Hotel_Day1,
    Hotel_Day2
}
public class FadeManager : MonoBehaviour
{
    [TitleGroup("FadeManager", "MonoBehaviour", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
    [SerializeField] FadeEffect fadeEffect;

    [TabGroup("Tab", "Scene", SdfIconType.Film, TextColor = "white")]
    [TabGroup("Tab", "Scene")][SerializeField] GameObject[] sceneLoadings;
    [TabGroup("Tab", "Scene")][SerializeField] GameObject loadingBar;

    public event Action fadeComplete;

    public void EventActionClear()
    {
        fadeComplete = null;
    }
    public IEnumerator FadeStart(FadeState fadeState)
    {
        yield return StartCoroutine(Fade(fadeState));
    }

    public void FadeImmediately()
    {
        fadeEffect.FadeImmediately();
    }
    public void MoveScene(SceneEnum sceneEnum)
    {
        StartCoroutine(MoveSceneFade(sceneEnum));
    }
    private IEnumerator Fade(FadeState fadeState)
    {
        Debug.Log("??");
        switch (fadeState)
        {
            case FadeState.FadeOut:
                yield return fadeEffect.UseFadeEffect(FadeState.FadeOut);
                break;
            case FadeState.FadeIn:
                yield return fadeEffect.UseFadeEffect(FadeState.FadeIn);
                break;
            case FadeState.FadeOutIn:
                yield return fadeEffect.UseFadeEffect(FadeState.FadeOut);
                yield return fadeEffect.UseFadeEffect(FadeState.FadeIn);
                break;
            case FadeState.FadeInOut:
                yield return fadeEffect.UseFadeEffect(FadeState.FadeIn);
                yield return fadeEffect.UseFadeEffect(FadeState.FadeOut);
                break;
        }
        fadeComplete?.Invoke();
        fadeComplete = null;
        fadeEffect.OffFadeObject();
    }
    public bool loadComplete;
    public int sceneIndex;
    private IEnumerator MoveSceneFade(SceneEnum sceneEnum)
    {
        yield return Fade(FadeState.FadeOut);

        int rand = UnityEngine.Random.Range(0, sceneLoadings.Length);
        sceneLoadings[rand].SetActive(true);
        loadingBar.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)sceneEnum);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3);
        sceneLoadings[rand].SetActive(false);
        loadingBar.SetActive(false);
        yield return Fade(FadeState.FadeIn);
    }
}
