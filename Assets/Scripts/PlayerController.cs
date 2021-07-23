using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform attackPivot;     // 공격 점 기준.
    [SerializeField] float attackDistance;      // 공격 점 위치.
    [SerializeField] float attackRange;         // 공격 범위.
    [SerializeField] float attackRate;          // 공격 주기(속도).
    [SerializeField] LayerMask enemyMask;       // 적의 레이어.

    [SerializeField] Movement movement;     // 움직임 제어.    
    [SerializeField] AudioSource jumpSFX;   // 점프 효과음.

    const int MAX_JUMP_COUNT = 2;

    int jumpCount = 0;
    bool isAttack = false;
    bool isRight = true;

    new SpriteRenderer renderer;    // 스프라이트 렌더러.
    Animator anim;                  // 애니메이터.

    void Start()
    {
        jumpCount = MAX_JUMP_COUNT;

        renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
            renderer.flipX = true;
        }
        // 오른쪽을 눌렀다면.
        else if (horizontal >= 1.0f)
        {
            renderer.flipX = false;
        }

        isRight = (renderer.flipX == false);

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


    public void OnAttack()
    {
        Vector3 dir = isRight ? Vector2.right : Vector2.left;
        dir *= attackDistance;

        // Enemy레이어를 달고 있는 모든 콜리더를 검색한다.
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPivot.position + dir, attackRange, enemyMask);
        foreach(Collider2D collider in hitEnemys)
        {
            Damageable target = collider.GetComponent<Damageable>();
            if(target != null)
            {
                // 데미지 관련 메세지를 작성해서 전달.
                Damageable.DamageMessage message;
                message.attacker = transform;
                message.attackerName = "플레이어";
                message.amount = 1;

                target.OnDamaged(message);
            }
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

    private void OnDrawGizmosSelected()
    {
        // 기즈모 : 씬 창의 아이콘 같은 것.
        if (attackPivot != null)
        {
            Gizmos.color = Color.red;
            Vector3 dir = isRight ? Vector2.right : Vector2.left;
            dir *= attackDistance;
            Gizmos.DrawWireSphere(attackPivot.position + dir, attackRange);
        }
    }
}
