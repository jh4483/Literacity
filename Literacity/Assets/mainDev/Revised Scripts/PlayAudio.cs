using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private AudioSource audioSource;
    SpreadSheetNew spreadSheetNew;
    DragBall dragBall;

    void Start()
    {
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
        dragBall = FindObjectOfType<DragBall>();
        audioSource = spreadSheetNew.ballAudioSource;
    }

    public void OnClick()
    {
        if (audioSource != null && audioSource.clip != null && !dragBall.isDragging)
        {
            audioSource.Play();
        }

        else
        {
            audioSource.Stop();
        }
    }
}
