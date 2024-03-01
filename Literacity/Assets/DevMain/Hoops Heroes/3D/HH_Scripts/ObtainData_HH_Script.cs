using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ObtainData_HH_Script : MonoBehaviour
{
    [SerializeField]
    public List <string> ballValues = new List<string>();
    [SerializeField]
    public List <string> buttonValues = new List<string>();
    LoadLevelData_HH_Script loadLevelData;
    AssignData_HH_Script assignData;
    void Start()
    {
        loadLevelData = FindObjectOfType<LoadLevelData_HH_Script>();
        assignData = FindObjectOfType<AssignData_HH_Script>();
        StartCoroutine(AccessData());
    }

    void Update()
    {
        
    }

    IEnumerator AccessData()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1XkmsYBKhnmxeO84p4bnrXEAhbOCCYD49KuVfeypkDCM/values/Sheet1?key=AIzaSyCdKG3pwBztFhkiVB12O5eqOZbsDk7g0ps");
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Network Error");
        }

        else
        {
            string json = www.downloadHandler.text;
            var o = JSON.Parse(json);
            
            foreach(var item in o["values"])
            {
                var levelData = JSON.Parse(item.ToString());
                if(levelData[0][0] == loadLevelData.levelName)
                {
                    buttonValues.Add(levelData[0][1]);
                    ballValues.Add(levelData[0][2]);
                    ballValues.Add(levelData[0][3]);
                    assignData.AddBallData();
                }

                else
                {
                    yield return null;
                }
            }

            assignData.AddButtonData();
        }
    }
}
