using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] Animator anim;

    float horizontal;

    private void Start()
    {
        horizontal = 1f;
    }

    void Update()
    {
        movement.OnMove(horizontal);
        anim.SetBool("isMove", movement.isMove);
    }
}
