using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;

public class SpreadSheetNew : MonoBehaviour
{
    public List<string> ballLetters = new List<string>();
    public List <string> words = new List<string>();
    public List <string> roundNumber = new List<string>();
    public List<Button> ballOrder = new List<Button>();    
    public RectTransform origin;
    public Button basketBall;
    public AudioSource ballAudioSource;

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

        foreach (RoundData roundData in roundDataList)
        {
            ballLetters.Add(roundData.BallLetters);
            if(roundData.Word != "")
            {
                words.Add(roundData.Word);
            }
            if(roundData.Round != "")
            {
                roundNumber.Add(roundData.Round);
            }
        }

        for (int i = 0; i < ballLetters.Count; i++)
        {
            yield return new WaitForSeconds(0.3f);
            Button newBall = Instantiate(basketBall, origin);
            newBall.transform.SetParent(origin);
            newBall.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ballLetters[i].ToString();
            float xOffset = 60f;
            Vector2 anchoredPosition = new Vector2(-150f + (i * xOffset), 10f);
            newBall.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
            string audioFileName = ballLetters[i];
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
                Debug.LogWarning("Audio clip not found for ball: " + ballLetters[i]);
            }
        }
    }
}
