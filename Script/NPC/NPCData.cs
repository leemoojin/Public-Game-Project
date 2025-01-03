using UnityEngine;

[CreateAssetMenu]
public class NPCData : ScriptableObject
{
    [SerializeField] private SerializableDictionary<string, string> Description;
    [SerializeField] private SerializableDictionary<string, Sprite> Face;
    
    //NPC 동적 생성 방식 채택 시 사용
    //public Mesh Model;
    //public Material Mat;

    public bool GetDescription(string key, out string result)
    {
        return Description.TryGetValue(key, out result);
    }

    public bool GetFace(string key, out Sprite face)
    {
        return Face.TryGetValue(key, out face);
    }
}