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

public class VisualCues : MonoBehaviour
{    
    [SerializeField] private GameObject wordBlockParent;
    [SerializeField] private GameObject fingerPointer;
    public List<GameObject> wordBlockPos = new List<GameObject>();
    public List<TextMeshProUGUI> wordBlockTexts = new List<TextMeshProUGUI>();
    public int moveFingerIndex;
    private string line;
    public Image backgroundImage;
    public Color originalColor;
    public Color pausedStateColor;
    private int currentLine;
    private int sentenceLength;
    public bool sentenceCompleted;
    public bool pausedStateOn;
    Whisper whisper;
    LoadSentences loadSentences;

    void Start()
    {
        whisper = FindObjectOfType<Whisper>();
        loadSentences = FindObjectOfType<LoadSentences>();
        moveFingerIndex = 0;
        currentLine = 0;
        SentenceRequirements();
        sentenceCompleted = false;
        pausedStateOn = false;
    }

    void Update()
    {
        if(sentenceCompleted && Input.GetMouseButtonDown(0))
        {
            currentLine++;
            SentenceRequirements();
            whisper.StartRecording();
        }
    }

    public void SentenceRequirements()
    {
        sentenceCompleted = false;  
        line = loadSentences.storySentences[currentLine];
        int sentenceLength = loadSentences.storySentences[currentLine].Split(' ').Length;
        moveFingerIndex = 0;
        fingerPointer.transform.rotation = Quaternion.Euler(0, 0, 0);
        fingerPointer.GetComponent<RectTransform>().anchoredPosition = new Vector3(-104, 127, 0);
        foreach(GameObject child in wordBlockPos)
        {
            child.GetComponent<Image>().color = originalColor;
            child.SetActive(false);
            child.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
        }
        PopulateSentence(line, sentenceLength);
    }

    public void PopulateSentence(string line, int numberOfWords)
    {
        wordBlockPos.Clear();
        float anchoredPositionXMultiplier = -3.1f;
        Vector2 initialAnchoredPosition = new Vector2(line.Length * anchoredPositionXMultiplier, wordBlockParent.GetComponent<RectTransform>().anchoredPosition.y);
        wordBlockParent.GetComponent<RectTransform>().anchoredPosition = initialAnchoredPosition;

        string[] words = line.Split(' ');
        float xOffset = 0;
        float gapBetweenWords = 5f;
        
        for(int i = 0; i < numberOfWords; i++)
        {
            wordBlockPos.Add(wordBlockParent.transform.GetChild(i).gameObject);
            wordBlockPos[i].SetActive(true);

            TextMeshProUGUI wordBlockText = wordBlockPos[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            wordBlockText.text = words[i]; 
            wordBlockTexts.Add(wordBlockText);

            Vector2 wordBlockSize = wordBlockPos[i].GetComponent<RectTransform>().sizeDelta;
            wordBlockSize.x = wordBlockText.preferredWidth + gapBetweenWords;
            wordBlockPos[i].GetComponent<RectTransform>().sizeDelta = wordBlockSize;
        }

        StartCoroutine(MoveFinger());
    }


    public IEnumerator MoveFinger()
    {
        yield return new WaitForSeconds(0.1f);
        RectTransform fingerPointerRectTransform = fingerPointer.GetComponent<RectTransform>();
        StartCoroutine(FingerPointerAnimation());

        ColorChange();
        if (moveFingerIndex < 0 || moveFingerIndex >= wordBlockPos.Count || wordBlockPos[moveFingerIndex] == null)
        {
            sentenceCompleted = true;
            ResetGame();
            yield break;
        }

        else
        {
            RectTransform targetRectTransform = wordBlockPos[moveFingerIndex].GetComponent<RectTransform>();     
            wordBlockPos[moveFingerIndex].GetComponent<Button>().enabled = true;
            float duration = 0.3f; 
            float elapsed = 0.0f;

            Vector2 initialPosition = fingerPointerRectTransform.position;
            Vector2 targetPosition = new Vector2(targetRectTransform.position.x, initialPosition.y);

            while (elapsed < duration)
            {
                fingerPointerRectTransform.position = Vector2.Lerp(initialPosition, targetPosition, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            fingerPointerRectTransform.position = targetPosition;
        }

    }

    public void ColorChange()
    {
        if(whisper.hasSpoken && whisper.hasClicked) 
        {
            wordBlockPos[moveFingerIndex - 1].GetComponent<Image>().color = Color.grey;
            wordBlockPos[moveFingerIndex - 1].GetComponent<Button>().enabled = false;
        }

        else if(!whisper.hasSpoken && whisper.hasClicked)
        {
            wordBlockPos[moveFingerIndex].GetComponent<Image>().color = Color.blue;
        }

    }

    private IEnumerator FingerPointerAnimation()
    {
        GameObject animationOne = fingerPointer.transform.GetChild(0).gameObject;
        GameObject animationTwo = fingerPointer.transform.GetChild(1).gameObject;
        GameObject animationThree = fingerPointer.transform.GetChild(2).gameObject;

        animationOne.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        animationTwo.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        animationThree.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        animationOne.SetActive(false);
        animationTwo.SetActive(false);
        animationThree.SetActive(false);
    }

    public void PausedState()
    {
        if(!sentenceCompleted)
        {
            backgroundImage.color = pausedStateColor;
            pausedStateOn = true;
            ResetGame();
        }
    }

    private void ResetGame()
    {
        RectTransform fingerPointerRectTransform = fingerPointer.GetComponent<RectTransform>();
        fingerPointer.transform.rotation = Quaternion.Euler(0, 0, 64f);
        fingerPointerRectTransform.anchoredPosition = new Vector3(-104, 147, 0);
        whisper.hasSpoken = false;
        whisper.hasClicked = false;
        whisper.isRecording = false;
        whisper.EndRecording();
    }
}

