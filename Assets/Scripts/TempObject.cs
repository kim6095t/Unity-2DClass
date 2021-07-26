using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempObject : MonoBehaviour
{
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(x, y, 0) * 2f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("콜리즌");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("트리거 Enter");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("트리거 Stay");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("트리거 Exit");
    }
}
