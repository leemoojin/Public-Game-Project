using System.ComponentModel.Design;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SaveLoad
{
    private static TimelineElement[] generator =
        { new ScriptGenerator(), new ConversationGenerator(), new SelectGenerator() };
    
    public static void LoadDialogue(TextAsset csv)
    {
        string json;
        
        if (CsvToJson.ConvertCsv(csv, out json))
        {
            JArray arr = JArray.Parse(json);

            foreach (var item in arr)
            {
                generator[int.Parse(item["Type"].ToString())].Element(item);
            }
        }
        else
        {
            Debug.LogError("csv 파일이 존재하지 않습니다.");
            throw CheckoutException.Canceled;
        }
    }
}
