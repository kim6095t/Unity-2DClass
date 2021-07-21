using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform groundChecker;   // 지면 체크용 위치 변수.
    [SerializeField] LayerMask groundMask;      // 지면 체크 마스크.
    [SerializeField] float checkerDistance;     // 체크 거리.

    [SerializeField] Rigidbody2D rigid;
    
    [SerializeField] float speed;
    [SerializeField] float jumpPower;

    float horizontal; // 좌, 우 방향.

    public bool isGround { get; private set; } // 내가 땅에 서 있을 때.
    public bool isMove
    {
        get
        {
            return horizontal != 0f;
        }
    }
    public float velocityY
    {
        get
        {
            return rigid.velocity.y;
        }
    }

    private void Update()
    {
        CheckGround();
        Move();
    }

    void CheckGround()
    {
        // 특정 위치에서 아래 방향으로 레이(광선)를 쏜다.
        RaycastHit2D hit = Physics2D.Raycast(groundChecker.position, Vector2.down, checkerDistance, groundMask);
        Debug.DrawRay(groundChecker.position, Vector2.down * checkerDistance, Color.red);

        isGround = hit;
    }
    void Move()
    {
        rigid.velocity = new Vector2(horizontal * speed, rigid.velocity.y);
    }

    public void OnMove(float horizontal)
    {
        this.horizontal = horizontal;
    }
    public void OnJump()
    {
        // 위쪽 방향으로 10만큼의 힘을 가하라.
        rigid.velocity = new Vector2(rigid.velocity.x, 0.0f);
        rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
    }
}
