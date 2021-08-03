using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    bool[] isClearStage;

    void Start()
    {
        DataManager.OnLoad += Load;
        DataManager.OnSave += Save;

        isClearStage = new bool[10];
        Load();
    }

    void Load()
    {
        for(int i = 0; i<10; i++)
        {
            string key = string.Concat("ClearStage", i);
            isClearStage[i] = DataManager.GetInt(key) == 1;
        }

        Debug.Log("StageData Load");

    }
    void Save()
    {
        for (int i = 0; i < 10; i++)
        {
            string key = string.Concat("ClearStage", i);
            DataManager.SetInt(key, isClearStage[i] ? 1 : 0);
        }

        Debug.Log("StageData Save");
    }
}
