using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;

public class SpreadSheetNew : MonoBehaviour
{
    public List<string> ballLettersList = new List<string>();
    public List <string> wordsList = new List<string>();
    public List <string> roundNumberList = new List<string>();
    public List<Button> ballOrder = new List<Button>();    
    public RectTransform origin;
    public Button basketBall;
    public AudioSource ballAudioSource;
    public Button[] wordImage;

    [System.Serializable]
    public class RoundData
    {
        public string Round;
        public string Word;
        public string BallLetters;
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<RoundData> items;
    }

    void Start()
    {
        StartCoroutine(LoadRoundData());
    }

    void Update()
    {

    }

    public IEnumerator LoadRoundData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "JSON File/revised_json.txt");
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
        List<RoundData> roundDataList = wrapper.items;


        // Adding necessary data to lists 
        foreach (RoundData roundData in roundDataList)
        {
            ballLettersList.Add(roundData.BallLetters);
            if(roundData.Word != "")
            {
                wordsList.Add(roundData.Word);
            }

            if(roundData.Round != "")
            {
                roundNumberList.Add(roundData.Round);
            }
        }

        for (int i = 0; i < ballLettersList.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Button newBall = Instantiate(basketBall, origin);
            newBall.transform.SetParent(origin);
            newBall.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ballLettersList[i].ToString();
            float xOffset = 60f;
            Vector2 anchoredPosition = new Vector2(-150f + (i * xOffset), 10f);
            newBall.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
            string audioFileName = ballLettersList[i];
            AudioClip audioClip = Resources.Load<AudioClip>(audioFileName);
            if (audioClip != null)
            {
                ballAudioSource = newBall.gameObject.AddComponent<AudioSource>();
                ballAudioSource.loop = false;
                ballAudioSource.playOnAwake = false;
                ballAudioSource.clip = audioClip;
            }
            else
            {
                Debug.LogWarning("Audio clip not found for ball: " + ballLettersList[i]);
            }
        }
        for(int i = 0; i < wordsList.Count; i++)
        {
            Button wordPrompt = Instantiate(wordImage[i]);
            wordPrompt.transform.SetParent(origin);
            float yOffset = 30f;
            Vector2 anchoredPosition = new Vector2(230f, 100f + (i * yOffset));
            wordPrompt.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        }
    }
}
