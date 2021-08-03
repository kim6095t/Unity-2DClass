using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singletone<PlayerData>
{
    public int lastStage = 0;

    private void Start()
    {
        DataManager.OnSave += OnSave;
        DataManager.OnLoad += OnLoad;

        OnLoad();

        DontDestroyOnLoad(gameObject);
    }

    void OnSave()
    {
        DataManager.SetInt("lastStage", lastStage);
    }
    void OnLoad()
    {
        lastStage = DataManager.GetInt("lastStage");
    }
}
