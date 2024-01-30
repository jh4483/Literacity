using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    VR_VoiceRecognition.VoiceRecognition voice = new VR_VoiceRecognition.VoiceRecognition();

	// Use this for initialization
	void Start () {
        if (voice.StartSpeechRecognition())
        {
            Debug.Log("Success!");
        } else
        {
            Debug.Log("Failure!");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
