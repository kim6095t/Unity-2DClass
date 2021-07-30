using System;
using System.Collections.Generic;
using static ItemObject;

public class Inventory
{
    Dictionary<ITEM, int> storage;

    public Inventory()
    {
        storage = new Dictionary<ITEM, int>();

        DataManager.Instance.OnSave += Save;
        DataManager.Instance.OnLoad += Load;

        Load();
    }

    private void CheckItem(ITEM item)
    {
        if (storage.ContainsKey(item) == false)
            storage.Add(item, 0);
    }

    private void Init(ITEM item, int count)
    {
        CheckItem(item);
        storage[item] = count;
    }
    public void Add(ITEM item)
    {
        // 딕셔너리의 키 값 여부 확인.
        CheckItem(item);
        storage[item] += 1;
    }
    public void Remove(ITEM item, int count)
    {
        CheckItem(item);
        storage[item] -= count;
    }
    public bool IsEnough(ITEM item, int count)
    {
        CheckItem(item);
        return storage[item] >= count;
    }

    public void Load()
    {
        DataManager dm = DataManager.Instance;
        Init(ITEM.Gem, dm.GetInt("Gem"));
        Init(ITEM.Cherry, dm.GetInt("Cherry"));
    }
    public void Save()
    {
        DataManager dm = DataManager.Instance;
        dm.SetInt("Gem", Count(ITEM.Gem));
        dm.SetInt("Cherry", Count(ITEM.Cherry));
    }

    public int Count(ITEM type)
    {
        CheckItem(type);
        return storage[type];
    }
}
