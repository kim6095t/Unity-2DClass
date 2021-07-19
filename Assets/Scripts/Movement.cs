using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform groundChecker;   // 지면 체크용 위치 변수.
    [SerializeField] LayerMask groundMask;      // 지면 체크 마스크.
    [SerializeField] float checkerDistance;     // 체크 거리.

    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Transform imagePivot;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;

    [SerializeField] AudioSource jumpSFX;

    public bool isGround { get; private set; } // 내가 땅에 서 있을 때.
    bool isAttack;
    bool isJump
    {
        get
        {
            return rigid.velocity.y != 0;
        }
    }

    void Start()
    {
        isGround = true;
        Debug.Log("Movement Start");
    }

    void Update()
    {
        // 특정 높이 이하로 내려가면 죽는다.
        if (IsDeadLine())
        {
            OnDead();
            return;
        }

        CheckGround();
        Move();
        Jump();
        Attack();
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
        // Horizontal : 수평
        // 유니티의 기본 수평 키인 좌,우 값에 따라 -1.0 ~ 1.0 사이의 값.
        //float horizontal = Input.GetAxis("Horizontal");

        // GetAxisRow : 1 or 0 or -1.
        float horizontal = 0.0f;
        
        // 공격중이 아닐 때 이동 키를 받아온다.
        if(!isAttack)
            horizontal = Input.GetAxisRaw("Horizontal");

        bool isRun = (horizontal != 0);

        // 왼쪽을 눌렀다면.
        if (horizontal <= -1.0f)
        {
            imagePivot.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
        // 오른쪽을 눌렀다면.
        else if (horizontal >= 1.0f)
        {
            imagePivot.eulerAngles = Vector3.zero;
        }

        rigid.velocity = new Vector2(horizontal * speed, rigid.velocity.y);

        anim.SetBool("isRun", isRun);
        anim.SetFloat("velocityY", rigid.velocity.y); // 리지드바디의 Y축 속도 값을 전달.
    }
    void Jump()
    {
        // 내가 스페이스 바를 눌렀을 때. 땅에 서 있을 때. 공격 중이 아닐 때.
        if (Input.GetKeyDown(KeyCode.Space) && isGround && !isAttack)
        {
            // 위쪽 방향으로 10만큼의 힘을 가하라.
            rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);

            // 점프 효과음 재생.
            jumpSFX.Play();
        }

        anim.SetBool("isGround", isGround); // 애니메이터의 파라미터 isGround를 갱신.
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isAttack && isGround && !isJump)
        {
            anim.SetTrigger("onAttack"); // onAttack을 트리거 하겠다.
            isAttack = true;

            // 1초뒤에 OnFinish를 불러라.
        }
    }
    public void OnFinishAttack()
    {
        isAttack = false;
    }

    bool IsDeadLine()
    {
        return transform.position.y <= -4f;
    }

    // 임시로 죽었을 때.
    void OnDead()
    {
        // 내 오브젝트를 꺼라.
        gameObject.SetActive(false);
    }
}
