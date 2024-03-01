using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AnsweChecker_HH_Script : MonoBehaviour
{
    [SerializeField]
    public string pickedButton;
    [SerializeField]
    public GameObject disableButton;
    private bool isCompleting;
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
            pointer = 0;        
            for(int i = 0; i < charCount; i++)
            {
                if(ballObserver.pickedWord[i] == pickedButton[i])
                {
                    pointer++;
                    addText.text += ballObserver.pickedWord[i];
                    isCompleting = true;
                    DisableButtons();
                }
            }
            
        }

        else if (isCompleting && pickedButton != "")
        {
            for(int i = 0; i < charCount; i++)
            {
                if(ballObserver.pickedWord[i] == pickedButton[pointer])
                {
                    pointer++;
                    addText.text += ballObserver.pickedWord[i];
                    isCompleting = false;
                }

                else return;
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

            else return;
        }
    }
}
