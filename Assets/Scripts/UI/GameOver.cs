using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] CanvasGroup group;

    private void OnEnable()
    {
        group.alpha = 0f;
        Invoke("OnShow", 3.0f);
    }

    void OnShow()
    {
        group.alpha = 1f;
    }

    public void OnRetry()
    {
        // ¹Ù²ï ÁöÁ¡        
        SceneMover.Instance.MoveScene("Game");
    }
}
