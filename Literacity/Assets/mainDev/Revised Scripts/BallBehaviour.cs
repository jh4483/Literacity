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
    ClickedPrompt clickedPrompt;
    SpreadSheetNew spreadSheetNew;
    GameObject[] enabledButtons;

    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
        clickedPrompt = FindObjectOfType<ClickedPrompt>();
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
    }

    private void OnCollisionEnter2D(Collision2D collider)
    { 

        if(collider.gameObject.name == "Basketball Ring")
        {
            transform.position = initialPos;
            transform.rotation = initialRot;
            enabledButtons = GameObject.FindGameObjectsWithTag("undone");
            if(checkText.GetComponent<TextMeshProUGUI>().text == spreadSheetNew.letterOneList[spreadSheetNew.targetIndex].ToString() && !spreadSheetNew.playNextRound)
            {
                GameObject selectedTarget = GameObject.Find((spreadSheetNew.targetIndex).ToString());
                selectedTarget.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = checkText.GetComponent<TextMeshProUGUI>().text.ToString();
                spreadSheetNew.playNextRound = true;

                for(int i = 0; i < enabledButtons.Length; i++)
                {
                    enabledButtons[i].gameObject.GetComponent<Button>().enabled = false;
                }
            }
            if(checkText.GetComponent<TextMeshProUGUI>().text == spreadSheetNew.letterTwoList[spreadSheetNew.targetIndex].ToString() && spreadSheetNew.playNextRound)
            {
                GameObject selectedTarget = GameObject.Find((spreadSheetNew.targetIndex).ToString());
                string existingText = selectedTarget.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
                selectedTarget.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = existingText + checkText.GetComponent<TextMeshProUGUI>().text.ToString();
                spreadSheetNew.playNextRound = false;
                selectedTarget.tag = "done";

                for(int i = 0; i < enabledButtons.Length; i++)
                {
                    if(enabledButtons[i].tag == "undone")
                    {
                        enabledButtons[i].gameObject.GetComponent<Button>().enabled = true;
                    }
                    
                }
            }
        }
    }
}

