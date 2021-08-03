using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬(장면) 관련 클래스.

public class TitleManager : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("NewGame");
        SceneManager.LoadScene("WorldMap");
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
