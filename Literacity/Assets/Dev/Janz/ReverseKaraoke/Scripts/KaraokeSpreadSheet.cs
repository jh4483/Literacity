using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;

public class KaraokeSpreadSheet : MonoBehaviour
{
    public TextMeshProUGUI lineText;
    public string[] lineSeparated;
    public List<string> lineWordsList = new List<string>();

    [System.Serializable]
    public class LineData
    {
        public int LineNumber;
        public string Line;
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<LineData> items;
    }

    void Start()
    {
        StartCoroutine(LoadLineData());
    }

    void Update()
    {
        
    }

    public IEnumerator LoadLineData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Hoops Heroes JSON/karaoke-json.txt");
        string json = "";

        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                json = www.downloadHandler.text;
            }
            else
            {
                Debug.LogError("Failed to load JSON file: " + www.error);
                yield break;
            }
        }
        else
        {
            if (File.Exists(filePath))
            {
                json = File.ReadAllText(filePath);
            }
            else
            {
                Debug.LogError("JSON file not found at path: " + filePath);
                yield break;
            }
        }

        json = "{\"items\":" + json + "}";
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        List<LineData> lineDataList = wrapper.items;

        foreach (LineData lineData in lineDataList)
        {
            lineWordsList.Add(lineData.Line);
        }

        for (int i = 0; i < lineDataList.Count; i++)
        {
            lineText.text = lineDataList[0].Line;
            lineSeparated = lineText.text.Split();
        }

    }


        //     string messageTextLower = message.text.ToLower();
        // string searchStringLower = karaokeSpreadSheet.lineSeparated[lineChecker].ToLower(); // Replace "your-search-string" with the string you want to search for

        // // Check if messageTextLower contains searchStringLower
        // if (messageTextLower.Contains(searchStringLower))
        // {
        //     Debug.Log("That's correct!");
        //     message.text = message.text + " " + res.Text;
        // }

        // else
        // {
        //     Debug.Log(message.text);
        // }
}