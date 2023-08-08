using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BallBehaviour : MonoBehaviour
{
    public TextMeshProUGUI checkText;
    public Vector2 initialPos;
    public Quaternion initialRot;
    public Image backboardHighlight;
    public AudioSource wordAudioSource;
    public AudioClip firstBallAudioSource;
    public AudioClip secondBallAudioSource;
    private Animation backboardScale;
    private Color backboardColor;
    ClickedPrompt clickedPrompt;
    SpreadSheetNew spreadSheetNew;
    GameObject[] enabledButtons;
    PlayButton playButton;
    BoosterState boosterState;
    PlayAudio playAudio;
    MoveButton moveButton;

    void Start()
    {
        playButton = FindObjectOfType<PlayButton>();
        backboardHighlight = playButton.basketballHighlight;
        initialPos = transform.position;
        initialRot = transform.rotation;
        clickedPrompt = FindObjectOfType<ClickedPrompt>();
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
        backboardScale = backboardHighlight.GetComponent<Animation>();
        boosterState = FindObjectOfType<BoosterState>();
        playAudio = GetComponent<PlayAudio>();
        moveButton = FindObjectOfType<MoveButton>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {         
        backboardColor = backboardHighlight.GetComponent<Image>().color;
        if (collider.gameObject.name == "Basketball Ring")
        {
            transform.position = initialPos;
            transform.rotation = initialRot;
            enabledButtons = GameObject.FindGameObjectsWithTag("undone");

            if (checkText.GetComponent<TextMeshProUGUI>().text == spreadSheetNew.letterOneList[spreadSheetNew.targetIndex].ToString() && !spreadSheetNew.playNextRound)
            {
                firstBallAudioSource = playAudio.audioSource.clip;
                playAudio.OnCollisionAudio();
                GameObject selectedTarget = GameObject.Find((spreadSheetNew.targetIndex).ToString());
                backboardScale.Play("Backboard Scaling");
                BoosterState.boosterPower++;             
                selectedTarget.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = checkText.GetComponent<TextMeshProUGUI>().text.ToString();
                spreadSheetNew.playNextRound = true;

                for (int i = 0; i < enabledButtons.Length; i++)
                {
                    enabledButtons[i].gameObject.GetComponent<Button>().enabled = false;
                }
            }
            else if (checkText.GetComponent<TextMeshProUGUI>().text == spreadSheetNew.letterTwoList[spreadSheetNew.targetIndex].ToString() && spreadSheetNew.playNextRound)
            {                

                StartCoroutine(CompletedWord());

            }
            else if (checkText.GetComponent<TextMeshProUGUI>().text != spreadSheetNew.letterOneList[spreadSheetNew.targetIndex].ToString() && !spreadSheetNew.playNextRound)
            {
                backboardScale.Play("Backboard Rotation");
                BoosterState.boosterPower--;
            }
            else if (checkText.GetComponent<TextMeshProUGUI>().text != spreadSheetNew.letterTwoList[spreadSheetNew.targetIndex].ToString() && spreadSheetNew.playNextRound)
            {
                backboardScale.Play("Backboard Rotation");
                BoosterState.boosterPower--;
            }
        }
    }

    private IEnumerator CompletedWord()
    {    

        playAudio.OnCollisionAudio();
        secondBallAudioSource = playAudio.audioSource.clip;
        GameObject selectedTarget = GameObject.Find((spreadSheetNew.targetIndex).ToString());

        string audioFileName = spreadSheetNew.wordsList[spreadSheetNew.targetIndex];

        yield return new WaitForSeconds(1.3f);

        AudioClip audioClip = Resources.Load<AudioClip>(audioFileName);
            
        if (audioClip != null)
        {
            wordAudioSource = selectedTarget.gameObject.AddComponent<AudioSource>();
            wordAudioSource.loop = false;
            wordAudioSource.playOnAwake = false;
            wordAudioSource.clip = audioClip;
            wordAudioSource.Play();
        }

        string existingText = selectedTarget.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        selectedTarget.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = existingText + checkText.GetComponent<TextMeshProUGUI>().text.ToString();
        spreadSheetNew.playNextRound = false;
        selectedTarget.tag = "done";
        backboardScale.Play("Backboard Scaling");
        BoosterState.boosterPower++;

        yield return new WaitForSeconds(2);
        for (int i = 0; i < enabledButtons.Length; i++)
        {
            if (enabledButtons[i].tag == "undone")
                {
                    enabledButtons[i].gameObject.GetComponent<Button>().enabled = true;
                }       
        }

        spreadSheetNew.selectedCard.GetComponent<CardAnim>().OnDone();
        spreadSheetNew.selectedCard.tag = "close";
        selectedTarget.GetComponent<RectTransform>().anchoredPosition = moveButton.originalPos;
    }
}

