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
    private GameObject basketBallRing;
    private AudioSource ballHitAudio;
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
        basketBallRing = GameObject.Find("Basketball Ring");
        ballHitAudio = basketBallRing.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!boosterState.hasCollided) 
        {
            boosterState.hasCollided = true; 
            backboardColor = backboardHighlight.GetComponent<Image>().color;
            if (collider.gameObject.name == "Basketball Ring")
            {
                StartCoroutine(HalfCompletedWord());
            }
        }
    }

    private IEnumerator HalfCompletedWord()
    {
        backboardScale.Play("Backboard Scaling");
        ballHitAudio.Play();
        transform.position = initialPos;
        transform.rotation = initialRot;
        enabledButtons = GameObject.FindGameObjectsWithTag("undone");

        if (checkText.GetComponent<TextMeshProUGUI>().text == spreadSheetNew.letterOneList[spreadSheetNew.targetIndex].ToString() && !spreadSheetNew.playNextRound)
        {
            boosterState.isCorrect = true;
            yield return new WaitForSeconds(0.7f);
            playAudio.OnCollisionAudio();
            GameObject selectedTarget = GameObject.Find((spreadSheetNew.targetIndex).ToString());
            BoosterState.boosterPower++;
            selectedTarget.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = checkText.GetComponent<TextMeshProUGUI>().text.ToString();
            boosterState.StartCoroutine(boosterState.PlayParticles());
            spreadSheetNew.playNextRound = true;

            for (int i = 0; i < enabledButtons.Length; i++)
            {
                enabledButtons[i].gameObject.GetComponent<Button>().enabled = false;
            }
        }
        else if (checkText.GetComponent<TextMeshProUGUI>().text == spreadSheetNew.letterTwoList[spreadSheetNew.targetIndex].ToString() && spreadSheetNew.playNextRound)
        {
            spreadSheetNew.playNextRound = false;
            StartCoroutine(CompletedWord());

        }
        else if (checkText.GetComponent<TextMeshProUGUI>().text != spreadSheetNew.letterOneList[spreadSheetNew.targetIndex].ToString() && !spreadSheetNew.playNextRound)
        {
            backboardScale.Play("Backboard Rotation");
            if (BoosterState.boosterPower != 0)
            {
                BoosterState.boosterPower = 0;
                boosterState.isCorrect = false;
            }
            boosterState.hasCollided = false;
        }
        else if (checkText.GetComponent<TextMeshProUGUI>().text != spreadSheetNew.letterTwoList[spreadSheetNew.targetIndex].ToString() && spreadSheetNew.playNextRound)
        {
            backboardScale.Play("Backboard Rotation");
            BoosterState.boosterPower = 0;
            boosterState.isCorrect = false;
        }
            boosterState.hasCollided = false;
    }

    private IEnumerator CompletedWord()
    {
        backboardScale.Play("Backboard Scaling");
        boosterState.isCorrect = true;
        yield return new WaitForSeconds(0.7f);
        playAudio.OnCollisionAudio();
        GameObject selectedTarget = GameObject.Find((spreadSheetNew.targetIndex).ToString());
        string existingText = selectedTarget.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        selectedTarget.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = existingText + checkText.GetComponent<TextMeshProUGUI>().text.ToString();

        yield return new WaitForSeconds(1.5f);
        selectedTarget.gameObject.GetComponent<AudioSource>().Play();

        BoosterState.boosterPower++;
        boosterState.StartCoroutine(boosterState.PlayParticles());

        yield return new WaitForSeconds(boosterState.timeTaken + 0.2f);

        selectedTarget.tag = "done";
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
        spreadSheetNew.totalScore++;
    }
}
