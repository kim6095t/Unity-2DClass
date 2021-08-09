using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class GameDoor : MonoBehaviour
{
    Animation anim;

    void Start()
    {
        // 게임 매니저에게 이벤트 등록.
        GameManager.Instance.OnOpenDoor += OnOpenDoor;
        anim = GetComponent<Animation>();
    }

    void OnOpenDoor()
    {
        anim.Play();
    }
}
