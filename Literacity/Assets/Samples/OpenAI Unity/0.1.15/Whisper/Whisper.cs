using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;
using OpenAI;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.EventSystems;

public class Whisper : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    private int sampleRate = 44100;
    private float interval = 0.05f; 
    private readonly string fileName = "output.wav";
    public AudioClip clip;
    public bool isRecording;
    private bool hasTranscribed;
    public bool hasClicked;
    public bool hasSpoken;
    private float time;
    private OpenAIApi openai = new OpenAIApi();
    private string selectedMicrophone;
    private VisualCues visualCues;
    private const float noiseThreshold = 0.002f;


    public class AuthData
    {
        public string ApiKey;
    }

    private void Start()
    {

        hasTranscribed = false;
        hasClicked = false;
        hasSpoken = false;
        visualCues = FindObjectOfType<VisualCues>();

        string apiKey = "";
        openai = new OpenAIApi(apiKey);
        string[] availableDevices = Microphone.devices;
        foreach (var device in availableDevices)
        {
            dropdown.options.Add(new Dropdown.OptionData(device));
        }

        dropdown.onValueChanged.AddListener(ChangeMicrophone);
        var index = PlayerPrefs.GetInt("user-mic-device-index");
        
        dropdown.SetValueWithoutNotify(index);

    }
    private void ChangeMicrophone(int index)
    {
        PlayerPrefs.SetInt("user-mic-device-index", index);
    }
    public void StartRecording()
    {

        time = 0;
        isRecording = true;
        
        var index = PlayerPrefs.GetInt("user-mic-device-index");
        string selectedDevice = dropdown.options[index].text;
        string[] availableDevices = Microphone.devices;
        if (!System.Array.Exists(availableDevices, device => device == selectedDevice))
        {
            Debug.LogError("Selected microphone device does not exist: " + selectedDevice);
            return;
        }
        selectedMicrophone = selectedDevice;
        clip = Microphone.Start(selectedDevice, false, 100, 44100);
        if (clip == null)
        {
            Debug.LogError("Microphone initialization failed for device: " + selectedDevice);
        }
        else if (clip.length == 0)
        {
            Debug.LogError("Microphone clip is empty.");
        }
    }
    public async void EndRecording()
    {
        byte[] data = SaveWav.Save(fileName, clip);
        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() { Data = data, Name = "audio.wav" },
            Model = "whisper-1",
            Language = "en"
        };

        var res = await openai.CreateAudioTranscription(req);
        string cleanedText = res.Text.ToLower();

        Debug.Log(cleanedText);

        char[] punctuationMarks = { '?', '!', '.', ',', ':', ';', '-' };

        if (punctuationMarks.Any(p => cleanedText.Contains(p)))
        {
            cleanedText = string.Join("", cleanedText.Split(punctuationMarks));
        }

        for(int i = 0; i < visualCues.wordBlockTexts.Count; i++)
        {
            string clickedWord = visualCues.wordBlockTexts[i].text.ToLower();

            if (punctuationMarks.Any(p => clickedWord.Contains(p)))
            {
                clickedWord = string.Join("", clickedWord.Split(punctuationMarks));
            }

            if(cleanedText.Contains(clickedWord))
            {
                visualCues.wordBlockTexts[i].text = $"<color=green>{visualCues.wordBlockTexts[i].text}</color>";
            }

        }

        string audioURL = "https://example.com/path/to/audio.wav";
        hasTranscribed = true;
    }

    private void Update()
    {
        if (isRecording)
        {
            time += Time.deltaTime;
            if (time >= 1.0f)
            {
                EndRecording();
                time = 0;
            }        
       }
    }

    public bool IsSpeaking()
    {
        int sampleSize = Mathf.RoundToInt(interval * sampleRate);
        float[] samples = new float[sampleSize];
        
        int position = Microphone.GetPosition(selectedMicrophone) - (sampleSize + 1);
        if (position < 0)
        {
            return false;
        }

        clip.GetData(samples, position);

        float rmsValue = 0;
        foreach (float sample in samples)
        {
            rmsValue += Mathf.Pow(sample, 2);
        }

        rmsValue /= samples.Length;
        rmsValue = Mathf.Sqrt(rmsValue);

        return rmsValue > noiseThreshold;

    }

    public void DetectNoise()
    {
            hasClicked = true;
            if (IsSpeaking())
            {
                hasSpoken = true;
                visualCues.moveFingerIndex++;
                StartCoroutine(visualCues.MoveFinger());
            }
            else
            {
                hasSpoken = false;
                visualCues.ColorChange();
            }
    }
}
