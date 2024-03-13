using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class AnswerChecker_HH_Script : MonoBehaviour
{
    [SerializeField]
    public string pickedButton;
    [SerializeField]
    public GameObject disableButton;
    public bool isCorrect;
    public UnityEvent UpdateCounter;
    private bool isCompleting;
    private int counter;
    private int pointer;
    private TextMeshProUGUI addText;
    AssignData_HH_Script assignData;
    BallObserver_HH_Script ballObserver;
    void OnEnable()
    {
        Basketball_HH_Script.onBallHoop += ValidateAnswer; 
    }

    void OnDisable()
    {
        Basketball_HH_Script.onBallHoop -= ValidateAnswer; 
    }
    void Start()
    {
        assignData = FindObjectOfType<AssignData_HH_Script>();
        ballObserver = FindObjectOfType<BallObserver_HH_Script>();
        isCompleting = false;
        isCorrect = false;
    }

    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        pickedButton = assignData.buttonCollection[EventSystem.current.currentSelectedGameObject];
        disableButton = EventSystem.current.currentSelectedGameObject;
        addText = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void ValidateAnswer()
    {
        int charCount = ballObserver.pickedWord.Length;

        if(!isCompleting && pickedButton != "")
        {
            counter = 0;   
            pointer = 0;        
            for(int i = 0; i < charCount; i++)
            {
                if(ballObserver.pickedWord[i] == pickedButton[i])
                {
                    counter++;
                    pointer++;
                    addText.text += ballObserver.pickedWord[i];
                    isCompleting = true;
                    DisableButtons();
                }
            }

            if(counter > 0)
            {
                isCorrect = true;
                UpdateCounter.Invoke();
            }
            
        }

        else if (isCompleting && pickedButton != "")
        {
            counter = 0;
            for(int i = 0; i < charCount; i++)
            {
                if(ballObserver.pickedWord[i] == pickedButton[pointer])
                {  
                    counter++; 
                    pointer++;
                    addText.text += ballObserver.pickedWord[i];
                    isCompleting = false;
                }

                else return;
            }

            if(counter > 0)
            {
                isCorrect = true;
                UpdateCounter.Invoke();
            }

            pickedButton = "";
            disableButton.tag = "done";
            EnableButtons();
        }

    }

    private void DisableButtons()
    {
        for(int i = 0; i < assignData.buttonArray.Length; i++)
        {
            assignData.buttonArray[i].transform.GetComponent<Button>().enabled = false;
        }
    }

    private void EnableButtons()
    {
        for(int i = 0; i < assignData.buttonArray.Length; i++)
        {
            if(assignData.buttonArray[i].tag != "done")
            {
                assignData.buttonArray[i].transform.GetComponent<Button>().enabled = true;
            }

            else continue;
        }
    }
}
