using System.Collections.Generic;

public class Inventory
{
    Dictionary<ItemObject.ITEM, int> storage;

    public Inventory()
    {
        storage = new Dictionary<ItemObject.ITEM, int>();
    }

    private void CheckItem(ItemObject.ITEM item)
    {
        if (storage.ContainsKey(item) == false)
            storage.Add(item, 0);
    }
    public void Add(ItemObject.ITEM item)
    {
        // 딕셔너리의 키 값 여부 확인.
        CheckItem(item);
        storage[item] += 1;
    }
    public void Remove(ItemObject.ITEM item, int count)
    {
        CheckItem(item);
        storage[item] -= count;
    }
    public bool IsEnough(ItemObject.ITEM item, int count)
    {
        CheckItem(item);
        return storage[item] >= count;
    }

    public int Count(ItemObject.ITEM type)
    {
        CheckItem(type);
        return storage[type];
    }
}
