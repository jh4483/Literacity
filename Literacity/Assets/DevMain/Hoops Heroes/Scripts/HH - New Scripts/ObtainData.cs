using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ObtainData : MonoBehaviour
{

    void Start()
    {
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
                var itemo = JSON.Parse(item.ToString());
            }
        }
    }
}
