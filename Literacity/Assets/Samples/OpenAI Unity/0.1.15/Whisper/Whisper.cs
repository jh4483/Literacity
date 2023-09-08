using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;
using OpenAI;

public class Whisper : MonoBehaviour
{
    [SerializeField] private Button recordButton;
    [SerializeField] private Image progressBar;
    [SerializeField] private Text message;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private AudioSource audioSource;

    private readonly string fileName = "output.wav";
    private readonly int duration = 2;

    private AudioClip clip;
    private bool isRecording;
    private float time;
    private OpenAIApi openai = new OpenAIApi();
    private string selectedMicrophone;

    public int moveNextLine;

    KaraokeSpreadSheet karaokeSpreadSheet;


    public class AuthData
    {
        public string ApiKey;
    }

    private void Start()
    {
        moveNextLine = 0;
        karaokeSpreadSheet = FindObjectOfType<KaraokeSpreadSheet>();
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
        clip = Microphone.Start(selectedDevice, false, 4, 44100);

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
                FileData = new FileData() {Data = data, Name = "audio.wav"},
                Model = "whisper-1",
                Language = "en"
            };
            var res = await openai.CreateAudioTranscription(req);

        Debug.Log("Transcription complete");

        progressBar.fillAmount = 0;


        string messageTextLower = res.Text.ToLower();
        string searchStringLower = karaokeSpreadSheet.lineText.text.ToLower();
        Debug.Log(messageTextLower);
        Debug.Log(searchStringLower);

        if (messageTextLower.Contains(searchStringLower))
        {
            // Check if the answer is the same as the audio input 
            Debug.Log("That's correct!");
            karaokeSpreadSheet.lineText.GetComponent<Text>().color = Color.red;
        }

        else
        {
            Debug.Log(res.Text);
        }

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
            progressBar.fillAmount = time / 4;

            if (time >= 4)
            {
                time = 4;
                isRecording = false;
                EndRecording();
                // StartRecording();
            }
        }
    }
}