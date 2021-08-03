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

        DataManager.SaveAll();
    }
    public void OnStageFail()
    {
        StageFail?.Invoke();
    }

    public void OnRetry()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnWorldMap()
    {
        SceneManager.LoadScene("WorldMap");
    }
}
