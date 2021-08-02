using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singletone<GameManager>
{
    [SerializeField] Animation clearDoor;

    [SerializeField] UnityEvent StageClear;
    [SerializeField] UnityEvent StageFail;

    int keyCount = 0;
    void Start()
    {
        GameKey[] allKey = GameObject.FindObjectsOfType<GameKey>();
        keyCount = allKey.Length;

        foreach(GameKey key in allKey)
            key.OnGet += OnGetKey;
    }

    void OnGetKey()
    {
        keyCount -= 1;
        if(keyCount <= 0)
        {
            Debug.Log("Clear Stage");
            clearDoor.Play();
        }
    }

    public void OnStageClear()
    {
        StageClear?.Invoke();
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
