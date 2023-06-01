using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SpreadSheetAccess : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject blankPrefab;
    [SerializeField] GameObject filledPrefab;
    [SerializeField] GameObject backBoardPrefab;
    [SerializeField] RawImage wordImage;
    [SerializeField] List<GameObject> upperStrip = new List<GameObject>();
    [SerializeField] List<GameObject> lowerStrip = new List<GameObject>();
    public static int currentRound = 1;
    public static int guessedAnswer = 0;
    public static List<string> correctAnswers = new List<string>();

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
        public string ImageURL;
        public string CorrectAnswerOne;
        public string CorrectAnswerTwo;
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
            List<string> optionsList = new List<string>();
            List<string> imageList = new List<string>();

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

                    optionsList.Add(roundData.BackLetterOne);
                    optionsList.Add(roundData.BackLetterTwo);
                    optionsList.Add(roundData.BackLetterThree);
                    optionsList.Add(roundData.BackLetterFour);
                    imageList.Add(roundData.ImageURL);
                    correctAnswers.Add(roundData.CorrectAnswerOne);
                    correctAnswers.Add(roundData.CorrectAnswerTwo);
                }
            }

            // Adding letters to the upper List - missing and available
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
                upperStrip[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(-312, 135);
            }

            float xOffset = 110f;
            for (int j = 1; j < upperStrip.Count; j++)
            {
                float xPosition = upperStrip[j - 1].GetComponent<RectTransform>().anchoredPosition.x + xOffset;
                upperStrip[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, 135);
            }

            // Retrieving URL to be assigned to the image 
            
            string imageUrl = imageList[0];
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                wordImage.texture = texture;
            }
            else
            {
                Debug.LogError("Error loading image from URL: " + imageUrl);
            }
            
            // Adding letters to the backboard 

            for(int i = 0; i < optionsList.Count; i++)
            {
                GameObject obj;

                obj = Instantiate(backBoardPrefab, canvas.transform);
                lowerStrip.Add(obj);
                obj.transform.GetChild(0).GetComponent<Text>().text = optionsList[i];
                obj.name =  obj.transform.GetChild(0).GetComponent<Text>().text;
            }

            for (int j = 0; j < 1; j++)
            {
                lowerStrip[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(-312, -80);
            }

            for (int j = 1; j < upperStrip.Count; j++)
            {
                float xPosition = upperStrip[j - 1].GetComponent<RectTransform>().anchoredPosition.x + xOffset;
                lowerStrip[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, -80);
            }

        }
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<RoundData> items;
    }
}

