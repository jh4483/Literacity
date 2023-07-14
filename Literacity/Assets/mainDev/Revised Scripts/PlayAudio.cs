using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public bool isSelected;
    private AudioSource audioSource;
    SpreadSheetNew spreadSheetNew;

    void Start()
    {
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
        audioSource = spreadSheetNew.ballAudioSource;
        isSelected = false;
    }

    public void OnClick()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        else
        {
            Debug.Log("not here");
        }
    }
}
