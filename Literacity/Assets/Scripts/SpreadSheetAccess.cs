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
    [SerializeField] Image blueStrip;
    public static List<GameObject> upperStrip = new List<GameObject>();
    public static List<GameObject> lowerStrip = new List<GameObject>();
    public static List<string> correctAnswers = new List<string>();
    public static List<GameObject> fillableAnswers = new List<GameObject>();
    public static string audioWord;
    public static int currentRound = 1;
    public static int guessedAnswer = 0;

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

    
    void Start()
    {
        StartCoroutine(LoadRoundData());
    }
    
    
    public IEnumerator LoadRoundData()
    {
        string url = "https://drive.google.com/uc?export=download&id=1SozTgMxGMTog6efPz7BnzYn2Zv4bo5Jt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading JSON data: " + webRequest.error);
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
                    audioWord = roundData.Word;
                }
            }

            // Adding letters to the upper List - missing and available, adding the available to a new list
            for(int i = 0; i < lettersList.Count; i++)
            {
                GameObject obj;
                if(lettersList[i] == "")
                {
                    obj = Instantiate(blankPrefab, canvas.transform);
                    upperStrip.Add(obj);
                    fillableAnswers.Add(obj);
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
                upperStrip[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(-295, 132);
            }

            float minX = -300f;
            float maxX = 300f;

            float upperOffset = 90f;
            for (int j = 1; j < upperStrip.Count; j++)
            {
                float xPosition = Mathf.Clamp(upperStrip[j - 1].GetComponent<RectTransform>().anchoredPosition.x + upperOffset, minX, maxX);
                upperStrip[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, 132);
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
                lowerStrip[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(-312, -40);
                lowerStrip[j].transform.localScale = new Vector2(0.7f, 0.7f);
                
            }

            float lowerOffset = 200f;
            for (int j = 1; j < lowerStrip.Count; j++)
            {
                float xPosition = lowerStrip[j - 1].GetComponent<RectTransform>().anchoredPosition.x + lowerOffset;
                lowerStrip[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, -40);
                lowerStrip[j].transform.localScale = new Vector2(0.7f, 0.7f);
            }

        }
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<RoundData> items;
    }

    public static void ClearAllLists()
    {
        foreach (GameObject obj in upperStrip)
        {
            Destroy(obj);
        }
        upperStrip.Clear();

        foreach (GameObject obj in lowerStrip)
        {
            Destroy(obj);
        }
        lowerStrip.Clear();

        correctAnswers.Clear();
        fillableAnswers.Clear();

    }
}

