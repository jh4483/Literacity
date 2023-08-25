using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace OpenAI
{
    public class Whisper : MonoBehaviour
    {
        [SerializeField] private Button recordButton;
        [SerializeField] private Image progressBar;
        [SerializeField] private Text message;
        [SerializeField] private Dropdown dropdown;

        private readonly string fileName = "output.wav";
        private readonly int duration = 1;

        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi();

        public class AuthData
        {
            public string ApiKey;
        }

        private void Start()
        {
            // Load the API key from auth.json
            string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "Hoops Heroes JSON/auth.json.txt");

            if (File.Exists(jsonFilePath))
            {
                // Read the API key from the JSON file
                using (StreamReader reader = new StreamReader(jsonFilePath))
                {
                    string jsonContents = reader.ReadToEnd();
                    var jsonData = JsonUtility.FromJson<AuthData>(jsonContents);
                    string apiKey = jsonData.ApiKey.Trim();

                    openai = new OpenAIApi(apiKey);

                    Debug.Log(apiKey);
                }
            }

            // Populate the microphone dropdown list
            foreach (var device in Microphone.devices)
            {
                dropdown.options.Add(new Dropdown.OptionData(device));
            }
            recordButton.onClick.AddListener(StartRecording);
            dropdown.onValueChanged.AddListener(ChangeMicrophone);

            var index = PlayerPrefs.GetInt("user-mic-device-index");
            dropdown.SetValueWithoutNotify(index);
        }

        private void ChangeMicrophone(int index)
        {
            PlayerPrefs.SetInt("user-mic-device-index", index);
        }

        private void StartRecording()
        {
            isRecording = true;
            recordButton.enabled = false;

            var index = PlayerPrefs.GetInt("user-mic-device-index");

            string selectedDevice = dropdown.options[index].text;

            string[] availableDevices = Microphone.devices;

            if (!System.Array.Exists(availableDevices, device => device == selectedDevice))
            {
                // The selected microphone device does not exist.
                Debug.LogError("Selected microphone device does not exist: " + selectedDevice);
                return;
            }

            clip = Microphone.Start(selectedDevice, false, duration, 44100);

            if (clip == null)
            {
                // Microphone initialization failed.
                Debug.LogError("Microphone initialization failed for device: " + selectedDevice);
            }
            else
            {
                Debug.Log("Microphone recording started on device: " + selectedDevice);
            }
        }

        private async void EndRecording()
        {
            message.text = "Transcribing...";

            // Remove the conditional check
            Microphone.End(null);

            if (clip == null)
            {
                // Handle the case where clip is null (e.g., show an error message).
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

            progressBar.fillAmount = 0;
            message.text = res.Text;
            recordButton.enabled = true;
        }

        private void Update()
        {
            if (isRecording)
            {
                time += Time.deltaTime;
                progressBar.fillAmount = time / duration;

                if (time >= duration)
                {
                    time = 0;
                    isRecording = false;
                    EndRecording();
                }
            }
        }
    }
}
