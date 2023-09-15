using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class TextInput : MonoBehaviour
{
    public InputField inputField;
    public Text textOutput;
    public TextMeshProUGUI wordCount;

    void Start()
    {
        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    void OnInputValueChanged(string inputText)
    {
        textOutput.text = inputText;
        string[] words = inputText.Split(new char[] { ' ', '\t', '\n' });
        int wordCountValue = words.Count(word => !string.IsNullOrEmpty(word));
        
        wordCount.text = wordCountValue.ToString();
    }
}
