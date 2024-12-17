using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{
    [TabGroup("Tab", "Manager", SdfIconType.GearFill, TextColor = "orange")]
    [TabGroup("Tab", "Manager")] public FadeManager fadeManager;

    protected override void Awake()
    {
        base.Awake();

        if (fadeManager == null) fadeManager = GetComponent<FadeManager>();
    }
}
