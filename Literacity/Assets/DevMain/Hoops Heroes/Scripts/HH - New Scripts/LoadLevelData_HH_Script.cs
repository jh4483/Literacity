using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelData_HH_Script : MonoBehaviour
{
    [SerializeField]
    public int levelNum;
    [SerializeField]
    public string levelName;
    void Start()
    {
       levelNum = 1;
       levelName = levelNum.ToString();
    }

    void Update()
    {
        
    }

    public void UpdateLevelData()
    {
        
    }
}
