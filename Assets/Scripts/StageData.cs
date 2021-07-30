using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    bool[] isClearStage;

    void Start()
    {
        DataManager.Instance.OnLoad += Load;
        DataManager.Instance.OnSave += Save;

        isClearStage = new bool[10];
        Load();
    }

    void Load()
    {
        DataManager dm = DataManager.Instance;
        for(int i = 0; i<10; i++)
        {
            string key = string.Concat("ClearStage", i);
            isClearStage[i] = dm.GetInt(key) == 1;
        }

        Debug.Log("StageData Load");

    }
    void Save()
    {
        DataManager dm = DataManager.Instance;
        for (int i = 0; i < 10; i++)
        {
            string key = string.Concat("ClearStage", i);
            dm.SetInt(key, isClearStage[i] ? 1 : 0);
        }

        Debug.Log("StageData Save");
    }
}
