using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Movement movement;     // 움직임 제어.
    [SerializeField] Animator anim;         // 애니메이터.
    [SerializeField] Transform imagePivot;  // 이미지의 피봇.
    [SerializeField] AudioSource jumpSFX;   // 점프 효과음.

    const int MAX_JUMP_COUNT = 2;
    
    bool isAttack;
    int jumpCount;

    void Start()
    {
        Debug.Log("Movement Start");
        jumpCount = MAX_JUMP_COUNT;
    }

    void Update()
    {
        // 특정 높이 이하로 내려가면 죽는다.
        if (IsDeadLine())
        {
            OnDead();
            return;
        }

        Move();
        Jump();
        Attack();

        anim.SetBool("isGround", movement.isGround);
        anim.SetBool("isMove", movement.isMove);
        anim.SetFloat("velocityY", movement.velocityY);
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // 좌:-1f, x:0f, 우:1f.
        if (isAttack)
            horizontal = 0f;

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

        movement.OnMove(horizontal);
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isAttack && jumpCount > 0)
        {
            movement.OnJump();
            jumpCount -= 1;
        }

        // 내가 떨어지고 있을때.
        if(movement.velocityY < 0.0f && movement.isGround)
        {
            jumpCount = MAX_JUMP_COUNT;
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isAttack && movement.isGround)
        {
            anim.SetTrigger("onAttack"); // onAttack을 트리거 하겠다.
            isAttack = true;
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
