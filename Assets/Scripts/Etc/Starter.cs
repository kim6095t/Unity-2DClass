using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] GameObject[] startObjects;

    // 전처리기 조건문.
    // 유니티 에디터상이 아니면 코드가 포함되지 않는다.
#if UNITY_EDITOR
    private void Awake()
    {
        for(int i= 0; i<startObjects.Length; i++)
        {
            GameObject findObject = GameObject.Find(startObjects[i].name);
            if (findObject == null)
            {
                GameObject newOjbect = Instantiate(startObjects[i]);
                string newName = newOjbect.name.Replace("(Clone)", string.Empty);
                newOjbect.name = newName;
            }
        }
    }
#endif

}
