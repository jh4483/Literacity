using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SpreadSheetAccess : MonoBehaviour
{
    private int currentRound = 1;
    public GameObject canvas;
    public GameObject blankPrefab;
    public GameObject filledPrefab;
    public List<GameObject> upperStrip = new List<GameObject>();

    [System.Serializable]
    public class RoundData
    {
        public int Round;
        public string Word;
        public string LetterOne;
        public string LetterTwo;
        public string LetterThree;
        public string LetterFour;
        public string LetterFive;
        public string BackLetterOne;
        public string BackLetterTwo;
        public string BackLetterThree;
        public string BackLetterFour;
        public string CorrectLetterOne;
        public string CorrectLetterTwo;
    }

    private IEnumerator Start()
    {
        string url = "https://drive.google.com/uc?export=download&id=1SozTgMxGMTog6efPz7BnzYn2Zv4bo5Jt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading JSON data");
                yield break;
            }

            string json = webRequest.downloadHandler.text;
            json = "{\"items\":" + json + "}";
            Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
            List<RoundData> roundDataList = wrapper.items;

            List<string> lettersList = new List<string>();

            foreach (RoundData roundData in roundDataList)
            {
                if (roundData.Round == currentRound)
                {
                    lettersList.Add(roundData.LetterOne);
                    lettersList.Add(roundData.LetterTwo);
                    lettersList.Add(roundData.LetterThree);
                    lettersList.Add(roundData.LetterFour);

                    if(roundData.LetterFive == "None")
                    {
                        
                    }

                    else
                    {
                        lettersList.Add(roundData.LetterFive);
                    }
                }
            }

            for(int i = 0; i < lettersList.Count; i++)
            {
                GameObject obj;
                if(lettersList[i] == "")
                {
                    obj = Instantiate(blankPrefab, canvas.transform);
                    upperStrip.Add(obj);
                    obj.transform.GetChild(0).GetComponent<Text>().text = lettersList[i];
                }
                else
                {
                    obj = Instantiate(filledPrefab, canvas.transform);
                    upperStrip.Add(obj);
                    obj.transform.GetChild(0).GetComponent<Text>().text = lettersList[i];
                }
            }

                for (int j = 0; j < 1; j++)
                {
                    upperStrip[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(-312, 151);
                }

                float xOffset = 150f;
                for (int j = 1; j < upperStrip.Count; j++)
                {
                    float xPosition = upperStrip[j - 1].GetComponent<RectTransform>().anchoredPosition.x + xOffset;
                    upperStrip[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, 151);
                }
        }
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<RoundData> items;
    }
}
