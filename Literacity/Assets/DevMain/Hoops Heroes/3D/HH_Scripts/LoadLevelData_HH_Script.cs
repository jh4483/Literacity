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
       levelName = levelNum.ToString();
    }

    void Update()
    {
        
    }

    public void UpdateLevelData()
    {
        
    }
}
