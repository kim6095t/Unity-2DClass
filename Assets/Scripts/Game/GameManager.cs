using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singletone<GameManager>
{
    [SerializeField] Transform stageParent;
    [SerializeField] ItemObject.ITEM whatIsKey;

    [SerializeField] UnityEvent StageClear;
    [SerializeField] UnityEvent StageFail;

    public event System.Action OnOpenDoor;              // 문이 열리는 이벤트.

    int keyCount = 0;

    protected new void Awake()      // 부모 클래스의 Awake를 가린다.
    {
        base.Awake();               // 부모 클래스 Singletone의 Awake 호출.

        int stageNumber = PlayerData.Instance.lastStage;
        for(int i = 0; i<stageParent.childCount; i++)
        {
            Transform stage = stageParent.GetChild(i);    // i번째 자식 오브젝트 대입.
            stage.gameObject.SetActive(i == stageNumber); // stageNumber 번째 오브젝트만 켠다.
        }
    }

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
                OnOpenDoor?.Invoke();           // 문 열림 이벤트 호출.
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
