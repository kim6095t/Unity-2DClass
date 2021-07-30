using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone<T> : MonoBehaviour
    where T : MonoBehaviour
{
    static T instance;
    public static T Instance => instance;

    private void Awake()
    {
        instance = GetComponent<T>();
    }
}
