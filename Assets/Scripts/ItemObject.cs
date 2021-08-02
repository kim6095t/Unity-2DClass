using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public enum ITEM
    {
        Gem,
        Cherry,
    }

    [SerializeField] ITEM itemType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");

        if(collision.tag == "Player")
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            pc.OnAddItem(itemType);

            Destroy(gameObject);                                            // 내 오브젝트를 지우겠다.
        }
    }
}
