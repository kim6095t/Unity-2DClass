using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.BGM.Forest1);
    }

    public void NewGame()
    {
        SceneMover.Instance.MoveScene("WorldMap");
    }
    public void OpenOption()
    {
        SceneMover.Instance.OpenOption();
    }
    public void ExitGame()
    {
        Debug.Log("ExitGame");
    }
}
