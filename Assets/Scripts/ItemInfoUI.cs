using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    static ItemInfoUI instance;
    public static ItemInfoUI Instance => instance;

    [SerializeField] Image gemImage;
    [SerializeField] Image cherryImage;
    [SerializeField] Text gemText;
    [SerializeField] Text cherryText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gemText.text = "0";
        cherryText.text = "0";
    }

    public void OnUpdateItem(ItemObject.ITEM type, int amount)
    {
        switch(type)
        {
            case ItemObject.ITEM.Gem:
                gemText.text = amount.ToString();
                break;
            case ItemObject.ITEM.Cherry:
                cherryText.text = amount.ToString();
                break;
        }
    }
}
