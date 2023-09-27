using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;
using OpenAI;
using TMPro;

public class Whisper : MonoBehaviour
{
    [SerializeField] private Button recordButton;
    // [SerializeField] private Image progressBar;
    [SerializeField] private Button stopButton;
    [SerializeField] private Text message;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI noiseText;
    [SerializeField] private TextMeshProUGUI transcribedText;
    [SerializeField] private Slider speedChecker;
    [SerializeField] private Text textOutput;
    [SerializeField] private TextMeshProUGUI textCount;

    private readonly string fileName = "output.wav";
    private readonly int duration = 2;
    private int redTextIndex = 0;

    private AudioClip clip;
    private bool isRecording;
    private bool hasTranscribed;
    private float time;
    private OpenAIApi openai = new OpenAIApi();
    private string selectedMicrophone;
    private float noiseThreshold = 0.1f;
    private float noiseDetectionInterval;
    private float timeSinceLastDetection = 0f;
    private bool hasDetectedNoise = false;
    private int noiseCount;
    private int currentSecond = 0; 
    private float clipStartTime = 0f; 
    private float clipEndTime = 0f; 
    private float noiseClipTimer = 0f; 
    private AudioClip noiseClip; 
    public int moveNextLine;
    StopRecording stopRecording;

    public class AuthData
    {
        public string ApiKey;
    }

    private void Start()
    {
        noiseCount = 0;
        moveNextLine = 0;
        hasTranscribed = false;
        stopRecording = FindObjectOfType<StopRecording>();
        string apiKey = "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";
        openai = new OpenAIApi(apiKey);

        string[] availableDevices = Microphone.devices;
        foreach (var device in availableDevices)
        {
            dropdown.options.Add(new Dropdown.OptionData(device));
        }
        recordButton.onClick.AddListener(StartRecording);
        dropdown.onValueChanged.AddListener(ChangeMicrophone);

        var index = PlayerPrefs.GetInt("user-mic-device-index");
        dropdown.SetValueWithoutNotify(index);

        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
    }

    private void ChangeMicrophone(int index)
    {
        PlayerPrefs.SetInt("user-mic-device-index", index);
    }

    private void StartRecording()
    {
        if(speedChecker.value == 0)
        {
            noiseDetectionInterval = 1.5f;
        }

        else if(speedChecker.value == 1)
        {
            noiseDetectionInterval = 1.0f;
        }

        else if(speedChecker.value == 2)
        {
            noiseDetectionInterval = 0.6f;
        }
        
        
        time = 0;
        isRecording = true;
        recordButton.enabled = false;

        Debug.Log("clicked");
        var index = PlayerPrefs.GetInt("user-mic-device-index");

        string selectedDevice = dropdown.options[index].text;

        string[] availableDevices = Microphone.devices;

        if (!System.Array.Exists(availableDevices, device => device == selectedDevice))
        {
            Debug.LogError("Selected microphone device does not exist: " + selectedDevice);
            return;
        }

        selectedMicrophone = selectedDevice;
        clip = Microphone.Start(selectedDevice, false, 15, 44100);

        if (clip == null)
        {
            Debug.LogError("Microphone initialization failed for device: " + selectedDevice);
        }
        else if (clip.length == 0)
        {
            Debug.LogError("Microphone clip is empty.");
        }
    }

    private async void EndRecording()
    {
        Microphone.End(selectedMicrophone);

        if (clip == null)
        {
            message.text = "Error: Recording clip is null.";
            recordButton.enabled = true;
            return;
        }

        byte[] data = SaveWav.Save(fileName, clip);

        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() { Data = data, Name = "audio.wav" },
            Model = "whisper-1",
            Language = "en"
        };
        var res = await openai.CreateAudioTranscription(req);

        Debug.Log("Transcription complete");

        if (res.Text.Contains("?") || res.Text.Contains("!") || res.Text.Contains(".") || res.Text.Contains(",") || res.Text.Contains(":") || res.Text.Contains(";") || res.Text.Contains("-"))
        {
            res.Text = res.Text.Replace("?", "").Replace("!", "").Replace(".", "").Replace(",", "").Replace(":", "").Replace(";", "").Replace("-", "");
        }

        
        transcribedText.text = res.Text.ToLower();
        string[] inputWords = textOutput.text.Split(' ');
        string[] transcribedWords = transcribedText.text.Split(' ');
        int minLength = Mathf.Min(inputWords.Length, transcribedWords.Length);

        string coloredText = ""; 

        for (int i = 0; i < minLength; i++)
        {
            if (inputWords[i] == transcribedWords[i])
            {
                coloredText += $"<color=green>{transcribedWords[i]}</color> ";
            }
            else
            {
                coloredText += $"<color=red>{transcribedWords[i]}</color> "; 
            }
        }

        if (inputWords.Length > transcribedWords.Length)
        {
            for (int i = minLength; i < transcribedWords.Length; i++)
            {
                coloredText += $"<color=red>{transcribedWords[i]}</color> "; 
            }
        }
        else if (inputWords.Length < transcribedWords.Length)
        {
            for (int i = minLength; i < transcribedWords.Length; i++)
            {
                coloredText += $"<color=red>{transcribedWords[i]}</color> "; 
            }

        }

        transcribedText.text = coloredText;
        noiseText.text = transcribedWords.Length.ToString();

        string audioURL = "https://example.com/path/to/audio.wav";
        OnRecordingComplete(audioURL);

        isRecording = false;
        recordButton.enabled = true;
        hasTranscribed = true;
    }

    private void OnRecordingComplete(string audioUrl)
    {
        audioSource.clip = clip;
        audioSource.enabled = true;
        stopRecording.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("replay");
    }

    public void PlayAudio()
    {
        Debug.Log("playing");
        audioSource.Play();
    }

    private void Update()
    {
        if (isRecording)
        {
            time += Time.deltaTime;
            // progressBar.fillAmount = time / 4;

            if (stopRecording.stopRecordingClicked)
            {
                isRecording = false;
                EndRecording();
            }

            timeSinceLastDetection += Time.deltaTime;
            if (!hasDetectedNoise && timeSinceLastDetection >= noiseDetectionInterval)
            {
                currentSecond = Mathf.FloorToInt(time);
                clipStartTime = Mathf.Max(currentSecond - 1, 0);
                clipEndTime = currentSecond;

                int clipSampleStart = Mathf.FloorToInt(clipStartTime * clip.frequency);
                int clipSampleEnd = Mathf.FloorToInt(clipEndTime * clip.frequency);

                float[] noiseSamples = new float[clipSampleEnd - clipSampleStart];
                clip.GetData(noiseSamples, clipSampleStart);

                bool hasNoise = false;
                for (int i = 0; i < noiseSamples.Length; i++)
                {
                    if (Mathf.Abs(noiseSamples[i]) > noiseThreshold)
                    {
                       hasNoise = true;
                       noiseCount++;
                       noiseText.text = noiseCount.ToString();
                       break;
                    }
                }

                if (hasNoise)
                {
                    hasDetectedNoise = true;
                }

                timeSinceLastDetection = 0f;
            }

            if (hasDetectedNoise && timeSinceLastDetection >= noiseDetectionInterval)
            {
                hasDetectedNoise = false;
                
            }
        }

        if(!isRecording && stopRecording.replayClicked)
        {
            transcribedText.text = "";
            textOutput.text = "";
            noiseText.text = "";
            noiseCount = 0;
            stopRecording.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Big button (2)");
            stopRecording.stopRecordingClicked = false;
            stopRecording.replayClicked = false;
        }
    }
}
