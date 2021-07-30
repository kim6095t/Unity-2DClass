using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{
    [SerializeField] Animation clearDoor;

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
}
