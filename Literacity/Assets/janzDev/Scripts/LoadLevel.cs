using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    private SpreadSheetAccess spreadSheetAccess;

    private void Start()
    {
        spreadSheetAccess = FindObjectOfType<SpreadSheetAccess>();
    }

    public IEnumerator LoadNextLevel()
    {
            yield return new WaitForSeconds(1f);
            SpreadSheetAccess.ClearAllLists();
            SpreadSheetAccess.guessedAnswer = 0;
            SpreadSheetAccess.currentRound++;
            StartCoroutine(spreadSheetAccess.LoadRoundData());
    }
}
