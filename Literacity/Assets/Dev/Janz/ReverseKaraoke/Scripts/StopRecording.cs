using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopRecording : MonoBehaviour
{
    public bool stopRecordingClicked;
    public bool replayClicked;

    void Start()
    {
        stopRecordingClicked = false;
    }
    public void StopRecordingClicked()
    {
        if(this.gameObject.GetComponent<Image>().sprite.name == "Big button (2)")
        {
            Debug.Log("stop clicked");
            stopRecordingClicked = true;
        }

        if(this.gameObject.GetComponent<Image>().sprite.name == "replay")
        {
            Debug.Log("replay clicked");
            replayClicked = true;
        }


    }
}
