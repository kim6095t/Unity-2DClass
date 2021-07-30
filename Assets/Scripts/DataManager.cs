using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singletone<DataManager>
{

    public event System.Action OnSave;
    public event System.Action OnLoad;

    public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
    public float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
    public void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public void SaveAll()
    {
        OnSave?.Invoke();
    }
    public void LoadAll()
    {
        OnLoad?.Invoke();
    }
}
