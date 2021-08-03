using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singletone<PlayerData>
{
    public int lastStage = 0;
    public bool[] isUnlockStages;  // 스테이지가 풀렸는가?
    public bool[] isStageClears;   // 스테이지를 클리어했는가?

    protected new void Awake()
    {
        base.Awake();

        isUnlockStages = new bool[GameData.MAX_STAGE_COUNT];
        isStageClears = new bool[GameData.MAX_STAGE_COUNT];

        DataManager.OnSave += OnSave;
        DataManager.OnLoad += OnLoad;

        OnLoad();
        DontDestroyOnLoad(gameObject);
    }

    void OnSave()
    {
        DataManager.SetInt("lastStage", lastStage);
        for(int i = 0; i< GameData.MAX_STAGE_COUNT; i++)
        {
            // 스테이지 클리어 여부.
            string key = string.Concat("isStageClears", i);
            DataManager.SetBool(key, isStageClears[i]);

            // 스테이지 언락 여부.
            key = string.Concat("isUnlockStages", i);
            DataManager.SetBool(key, isUnlockStages[i]);
        }
    }
    void OnLoad()
    {
        lastStage = DataManager.GetInt("lastStage");
        for(int i = 0; i< GameData.MAX_STAGE_COUNT; i++)
        {
            string key = string.Concat("isStageClears", i);
            isStageClears[i] = DataManager.GetBool(key);

            key = string.Concat("isUnlockStages", i);
            isUnlockStages[i] = DataManager.GetBool(key);
        }
    }
}
