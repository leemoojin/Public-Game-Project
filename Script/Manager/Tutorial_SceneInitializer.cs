using UnityEngine;

public class Tutorial_SceneInitializer : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GameManager.Instance.fadeManager.FadeStart(FadeState.FadeIn));
    }
}
