using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public enum ITEM
    {
        Gem,
        Cherry,

        Count,
    }

    [SerializeField] ITEM itemType;

    public ITEM ItemType => itemType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.Instance.OnGetKey(ItemType);
            PlayerController pc = collision.GetComponent<PlayerController>();
            pc.OnAddItem(itemType);

            Destroy(gameObject);                                            // 내 오브젝트를 지우겠다.
        }
    }
}
