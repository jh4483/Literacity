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
    [SerializeField] private Text message;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI noiseText;
    [SerializeField] private TextMeshProUGUI transcribedText;
    [SerializeField] private Slider speedChecker;
    [SerializeField] private Text textOutput;

    private readonly string fileName = "output.wav";
    private readonly int duration = 2;

    private AudioClip clip;
    private bool isRecording;
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

    public class AuthData
    {
        public string ApiKey;
    }

    private void Start()
    {
        noiseCount = 0;
        moveNextLine = 0;
        string apiKey = "";
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
        clip = Microphone.Start(selectedDevice, false, 7, 44100);

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


        // CHECK IF STRINGS MATCH WORD BY WORD - IF THEY MATCH, CHANGE COLOUR TO GREEN, IF NOT, CHANGE  TO RED.



        // string messageTextLower = res.Text.ToLower();
        transcribedText.text = res.Text.ToLower();
        // string searchStringLower = TextInput.textOutput.ToLower();
        // Debug.Log(messageTextLower);
        // Debug.Log(searchStringLower);

        // if (messageTextLower.Contains(searchStringLower))
        // {
        //     Debug.Log("That's correct!");
        //     karaokeSpreadSheet.lineText.GetComponent<Text>().color = Color.red;
        // }
        // else
        // {
        //     Debug.Log(res.Text);
        // }

        string audioURL = "https://example.com/path/to/audio.wav";
        OnRecordingComplete(audioURL);

        recordButton.enabled = true;
    }

    private void OnRecordingComplete(string audioUrl)
    {
        // audioSource.clip = clip;
        // audioSource.enabled = true;
        // audioSource.Play();
    }

    private void Update()
    {
        if (isRecording)
        {
            time += Time.deltaTime;
            // progressBar.fillAmount = time / 4;

            if (time >= 7)
            {
                time = 7;
                isRecording = false;
                EndRecording();
            }
            textOutput.GetComponent<Text>().color = Color.red;
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
            
            // textOutput.GetComponent<Text>().color = Color.black;

            if (hasDetectedNoise && timeSinceLastDetection >= noiseDetectionInterval)
            {
                hasDetectedNoise = false;
            }
        }
    }
}
