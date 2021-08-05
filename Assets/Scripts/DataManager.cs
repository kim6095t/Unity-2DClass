using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISave
{
    void OnSave();
    void OnLoad();
}

public static class DataManager
{
    public static event System.Action OnSave;
    public static event System.Action OnLoad;

    public static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
    public static float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }
    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1;
    }

    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
    public static void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public static void SetBool(string key, bool isBool)
    {
        PlayerPrefs.SetInt(key, isBool ? 1 : 0);
    }

    public static void SaveAll()
    {
        OnSave?.Invoke();
    }
    public static void LoadAll()
    {
        OnLoad?.Invoke();
    }
}
