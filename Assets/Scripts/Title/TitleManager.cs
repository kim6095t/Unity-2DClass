using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayBGM("Forest1");
    }

    public void NewGame()
    {
        SceneMover.Instance.MoveScene("WorldMap");
    }
    public void OpenOption()
    {
        Debug.Log("OpenOption");
    }
    public void ExitGame()
    {
        Debug.Log("ExitGame");
    }
}
