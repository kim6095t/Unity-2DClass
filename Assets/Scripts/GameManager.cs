using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singletone<GameManager>
{
    [SerializeField] Animation clearDoor;
    [SerializeField] ItemObject.ITEM whatIsKey;

    [SerializeField] UnityEvent StageClear;
    [SerializeField] UnityEvent StageFail;

    int keyCount = 0;
    void Start()
    {
        ItemObject[] allItems = FindObjectsOfType<ItemObject>();
        foreach(ItemObject item in allItems)
        {
            if (item.ItemType == whatIsKey)
                keyCount++;
        }
    }

    public void OnGetKey(ItemObject.ITEM item)
    {
        if(item == whatIsKey)
        {
            keyCount -= 1;
            if (keyCount <= 0)
            {
                Debug.Log("open door!");
                if (clearDoor != null)
                    clearDoor.Play();
            }
        }
    }

    public void OnStageClear()
    {
        StageClear?.Invoke();

        PlayerData player = PlayerData.Instance;            // 플레이어 정보 변수 선언.
        int currentStage = player.lastStage;                // 현재 스테이지 넘버.

        player.isStageClears[currentStage] = true;          // 현재 스테이지의 클리어 정보 갱신.

        if(currentStage < GameData.MAX_STAGE_COUNT - 1)     // 현재 스테이지가 마지막 스테이지가 아니라면.
            player.isUnlockStages[currentStage + 1] = true; // 다음 스테이지를 언락.

        DataManager.SaveAll();
    }
    public void OnStageFail()
    {
        StageFail?.Invoke();
    }

    public void OnRetry()
    {   
        SceneMover.Instance.MoveScene("Game");
    }
    public void OnWorldMap()
    {   
        SceneMover.Instance.MoveScene("WorldMap");
    }
}
