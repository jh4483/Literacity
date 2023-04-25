using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class LettersData 
{
    public string LetterOne;
    public string LetterTwo;
    public string LetterThree;
    public string LetterFour;
}

[System.Serializable]
public class Letters 
{
    public LettersData ReadLetters;
}

[System.Serializable]
public class BackboardData
{
    public string BackboardOne;
    public string BackboardTwo;
    public string BackboardThree;
    public string BackboardFour;
}

[System.Serializable]
public class Backboards
{
    public BackboardData BackboardLetters;
}

public class BasketballGame : MonoBehaviour
{
    public GameObject blankPrefab;
    public GameObject filledPrefab;
    public GameObject backboardPrefab;
    private List <GameObject> prefabList;
    private List<GameObject> backboardList;
    public GameObject upperStrip;
    public GameObject bottomStrip;

    string jsonURL = "https://drive.google.com/uc?export=download&id=1NeJrfEj2Iz8TYGn32c-DLd_v7lGML6ZZ";
    
    void Start()
    {
        prefabList = new List<GameObject>();
        backboardList = new List<GameObject>();
        StartCoroutine(GetData (jsonURL));
    }

    IEnumerator GetData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.Send();
        if (request.isNetworkError || request.isHttpError) 
        {
            Debug.Log("Could not retrieve Data");
        } 
        else 
        {    
            Letters data = JsonUtility.FromJson<Letters> (request.downloadHandler.text) ;
            Backboards letters = JsonUtility.FromJson<Backboards> (request.downloadHandler.text) ;
            foreach (var field in typeof(LettersData).GetFields()) 
            {
                if(field.GetValue(data.ReadLetters) == "")
                {
                    GameObject newPrefab = Instantiate(blankPrefab);
                    newPrefab.transform.parent = upperStrip.transform;
                    prefabList.Add(newPrefab);
                }

                if(field.GetValue(data.ReadLetters) != "")
                {
                    GameObject newPrefab = Instantiate(filledPrefab);
                    string fieldValue = (string)field.GetValue(data.ReadLetters);
                    newPrefab.transform.GetChild(0).GetComponent<Text>().text = fieldValue;
                    newPrefab.transform.parent = upperStrip.transform;
                    prefabList.Add(newPrefab);
                }
            }

            prefabList[0].transform.position = new Vector3(73.76f, 405.62f, 0.00f);
            for(int i = 1; i < prefabList.Count; i++)
            {
                prefabList[i].transform.position = new Vector3(prefabList[i-1].transform.position.x + 150.0f, prefabList[i-1].transform.position.y, prefabList[i-1].transform.position.z);
            }

            foreach (var field in typeof(BackboardData).GetFields()) 
            {
                string fieldValue = (string)field.GetValue(letters.BackboardLetters);
                GameObject newPrefab = Instantiate(backboardPrefab);
                newPrefab.transform.GetChild(0).GetComponent<Text>().text = fieldValue;
                newPrefab.transform.parent = bottomStrip.transform;
                backboardList.Add(newPrefab);
            }

            backboardList[0].transform.position = new Vector3(73.76f, 255.62f, 0.00f);
            for(int i = 1; i < backboardList.Count; i++)
            {
                backboardList[i].transform.position = new Vector3(backboardList[i-1].transform.position.x + 200.0f, backboardList[i-1].transform.position.y, backboardList[i-1].transform.position.z);
            }

      request.Dispose () ;
   } 
}
}
