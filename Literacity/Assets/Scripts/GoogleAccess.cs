using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public struct Data
{
    public string Name;
    public string ImageURL;
}

public class GoogleAccess : MonoBehaviour
{

    [SerializeField] Text nameText;
    [SerializeField] RawImage imageURL;

    string jsonURL = "https://drive.google.com/uc?export=download&id=1NeJrfEj2Iz8TYGn32c-DLd_v7lGML6ZZ";

    void Start()
    {
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
            Data data = JsonUtility.FromJson<Data> (request.downloadHandler.text) ;
            nameText.text = data.Name ;
            StartCoroutine (GetImage (data.ImageURL)) ;
        }

      request.Dispose () ;
   }

   IEnumerator GetImage (string url) 
   {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url) ;

        yield return request.SendWebRequest() ;

        if (request.isNetworkError || request.isHttpError) 
        {
            Debug.Log("Could not retrieve Data");
        }
        else 
        {
            imageURL.texture = ((DownloadHandlerTexture)request.downloadHandler).texture ;
        }
        request.Dispose () ;
   }

}