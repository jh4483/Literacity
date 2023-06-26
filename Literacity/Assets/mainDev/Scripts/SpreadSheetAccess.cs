using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;

public class SpreadSheetAccess : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject blueStrip;
    [SerializeField] GameObject blankPrefab;
    [SerializeField] GameObject filledPrefab;
    [SerializeField] GameObject[] backboardPrefab;
    [SerializeField] Image wordImage;
    public static List<GameObject> upperStrip = new List<GameObject>();
    public static List<GameObject> lowerStrip = new List<GameObject>();
    public static List<GameObject> fillableAnswers = new List<GameObject>();
    public static List<string> optionsList = new List<string>();
    public static List<string> correctAnswers = new List<string>();
    public static int currentRound;
    public static int guessedAnswer = 0;
    public static float upperOffset;
    public static string word;
    public Button playButton;
    public GameObject basketBall;
    LoadScene sceneLoader;

    [System.Serializable]
    public class RoundData
    {
        public int Round;
        public string Word;
        public string LetterOne;
        public string LetterTwo;
        public string BackLetterOne;
        public string BackLetterTwo;
        public string BackLetterThree;
        public string BackLetterFour;
        public string CorrectAnswerOne;
        public string ImageURL;
    }


    void Start()
    {
        sceneLoader = FindObjectOfType<LoadScene>();
        currentRound = 1;
    }

    void Update()
    {

    }

    public IEnumerator LoadRoundData()
    {
        if (currentRound == 6)
        {
            sceneLoader.playButton.gameObject.SetActive(true);
            playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("replay");
            blueStrip.SetActive(false);
            wordImage.gameObject.SetActive(false);
            for (int i = 0; i < backboardPrefab.Length; i++)
            {
                backboardPrefab[i].SetActive(false);
            }
            basketBall.SetActive(false);
            currentRound = 1;
            yield break;
        }

        string filePath = Path.Combine(Application.streamingAssetsPath, "JSON File/json.txt");
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

        List<string> lettersList = new List<string>();
        List<string> imageList = new List<string>();

        foreach (RoundData roundData in roundDataList)
        {
            if (roundData.Round == currentRound)
            {
                lettersList.Add(roundData.LetterOne);
                lettersList.Add(roundData.LetterTwo);

                // if(roundData.LetterFive == "None")
                // {

                // }

                // else
                // {
                //     lettersList.Add(roundData.LetterFive);
                // }

                optionsList.Add(roundData.BackLetterOne);
                optionsList.Add(roundData.BackLetterTwo);
                optionsList.Add(roundData.BackLetterThree);
                optionsList.Add(roundData.BackLetterFour);
                imageList.Add(roundData.ImageURL);
                correctAnswers.Add(roundData.CorrectAnswerOne);
                word = roundData.Word;

            }

        }
        yield return new WaitForSeconds(1);
        //adjust the size of the upperstrip 
        if (lettersList[1].Length == 3)
        {
            Debug.Log("over");
            upperOffset = 6.0f;
        }
        else if (lettersList[1].Length == 4)
        {
            Debug.Log("under");
            upperOffset = 6.6f;
        }
        // Adding letters to the upper List - missing and available, adding the available to a new list
        for (int i = 0; i < lettersList.Count; i++)
        {
            GameObject obj;
            if (lettersList[i] == "")
            {
                obj = Instantiate(blankPrefab, blueStrip.transform);
                upperStrip.Add(obj);
                fillableAnswers.Add(obj);
                obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = lettersList[i].ToString();
            }
            else
            {
                obj = Instantiate(filledPrefab, blueStrip.transform);
                upperStrip.Add(obj);
                obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = lettersList[i].ToString();
            }
        }

        for (int j = 0; j < 1; j++)
        {
            upperStrip[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(-3.7f, 0.70f);
        }

        for (int j = 1; j < upperStrip.Count; j++)
        {
            float xPosition = upperStrip[j - 1].GetComponent<RectTransform>().anchoredPosition.x + upperOffset;
            upperStrip[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, 0.70f);
        }

        for (int i = 0; i < optionsList.Count; i++)
        {
            backboardPrefab[i].transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = optionsList[i].ToString();
        }

        string imageName = currentRound.ToString();
        wordImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(imageName);

        yield break;
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<RoundData> items;
    }

    public IEnumerator ClearAllLists()
    {
        yield return new WaitForSeconds(2);

        foreach (GameObject obj in upperStrip)
        {
            Destroy(obj);
        }
        upperStrip.Clear();
        correctAnswers.Clear();
        fillableAnswers.Clear();
        optionsList.Clear();

        currentRound++;
        StartCoroutine(LoadRoundData());
    }
}
