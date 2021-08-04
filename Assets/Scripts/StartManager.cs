using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    void Start()
    {
        SceneMover.Instance.MoveScene("Title");

#if UNITY_STANDALONE_WIN

        Debug.Log("윈도우 버전 코드");

#elif UNITY_ANDROID

        Debug.Log("안드로이드용");

#endif

    }
}
