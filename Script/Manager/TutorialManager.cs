using Sirenix.OdinInspector;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private static TutorialManager _instance;
    public static TutorialManager Instance
    {
        get
        {
            _instance ??= FindAnyObjectByType<TutorialManager>();
            return _instance;
        }
    }

    [TabGroup("Tab", "Manager", SdfIconType.GearFill, TextColor = "orange")]
    [TabGroup("Tab", "Manager")] public Tutorial_SceneInitializer tutorial_SceneInitializer;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        if (tutorial_SceneInitializer == null) tutorial_SceneInitializer = GetComponent<Tutorial_SceneInitializer>();
    }
}
