using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
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
