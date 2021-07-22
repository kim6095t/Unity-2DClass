using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform headPivot;
    [SerializeField] float wallDistance;
    [SerializeField] LayerMask groundMask;

    [SerializeField] Movement movement;
    [SerializeField] Animator anim;
    [SerializeField] new SpriteRenderer renderer;

    float horizontal;  // 이동 방향.
    float height;      // 키.

    bool isRight
    {
        get
        {
            return horizontal >= 0;
        }
    }

    private void Start()
    {
        horizontal = 1f;

        // 키 = headPivot과 Enemy의 발(pivot) 까지의 거리.
        height = Vector2.Distance(headPivot.position, transform.position);
    }

    void Update()
    {
        CheckWall();
        CheckFliff();
        Move();
    }

    void CheckWall()
    {
        Vector2 direction = isRight ? Vector2.right : Vector2.left;
        if(Physics2D.Raycast(headPivot.position, direction, wallDistance, groundMask))
        {
            Debug.Log("앞에 벽이 있다!!");
            horizontal *= -1f;
        }
    }
    void CheckFliff()
    {
        Vector2 pivot = headPivot.position;
        Vector2 dir = isRight ? Vector2.right : Vector2.left;
        dir *= wallDistance;
        pivot += dir;

        Debug.DrawRay(pivot, Vector2.down * (height + .5f), Color.red);
        if(!Physics2D.Raycast(pivot, Vector2.down, height + .5f, groundMask))
        {
            Debug.Log("앗! 절벽이다.");
            horizontal *= -1f;
        }
    }
    void Move()
    {
        renderer.flipX = !isRight;               // 왼쪽으로 움직일 때 좌우 반전 시켜라.
        movement.OnMove(horizontal);             // movemnet에게 방향을 전달해서 움직여라.
        anim.SetBool("isMove", movement.isMove); // animator에게 isMove 파라미터를 갱신해라.
    }
}
