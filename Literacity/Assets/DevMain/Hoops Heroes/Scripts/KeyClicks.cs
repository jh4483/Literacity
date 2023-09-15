using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyClicks : MonoBehaviour
{
    public string keyClickIndex;

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.A)) 
       {
            keyClickIndex = "0";
            KeyClickAction();
       }

       if(Input.GetKeyDown(KeyCode.S)) 
       {
            keyClickIndex = "1";
            KeyClickAction();
       }

       if(Input.GetKeyDown(KeyCode.D)) 
       {
            keyClickIndex = "2";
            KeyClickAction();
       }

       if(Input.GetKeyDown(KeyCode.F)) 
       {
            keyClickIndex = "3";
            KeyClickAction();
       }
    }

    void KeyClickAction()
    {
        int index = int.Parse(keyClickIndex);
        Transform origin = GameObject.Find("Canvas").transform.Find("Origin");
        Transform child = origin.GetChild(index);
     
        if (child.tag == "done")
        {
          child.GetComponent<ClickedPrompt>().enabled = false;
        }

        else
        {
          ClickedPrompt clickedPrompt = child.GetComponent<ClickedPrompt>();
          if (clickedPrompt != null)
          {
               clickedPrompt.OnPromptClicked();
          }
        }

    }
}
