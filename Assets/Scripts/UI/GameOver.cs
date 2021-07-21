using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnRetry()
    {


        // ¹Ù²ï ÁöÁ¡
        SceneManager.LoadScene("Game");
    }
}
