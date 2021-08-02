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

    [SerializeField] float godModeTime;     // 무적 시간 타임.

    const int MAX_JUMP_COUNT = 2;

    int jumpCount = 0;
    bool isAttack = false;          // 내가 공격 중일 때.
    bool isRight = true;            // 나의 방향.
    bool isDamaged = false;         // 공격 받았을 때.

    bool isOnGoal = false;          // 내가 골에 서 있는가?
    bool isStopControl = false;     // 조작할 수 없는가?

    new SpriteRenderer renderer;    // 스프라이트 렌더러.
    Animator anim;                  // 애니메이터.

    Inventory inventory;            // 인벤토리 클래스.

    void Start()
    {
        jumpCount = MAX_JUMP_COUNT;

        renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.SetBool("isAlive", true);

        inventory = new Inventory();        
    }

    void Update()
    {
        if (isStopControl)
            return;

        // 특정 높이 이하로 내려가면 죽는다.
        if (IsDeadLine())
        {
            OnDead();
            return;
        }

        Move();
        Jump();
        Attack();
        Goal();

        anim.SetBool("isGround", movement.isGround);
        anim.SetBool("isMove", movement.isMove);
        anim.SetFloat("velocityY", movement.velocityY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            isOnGoal = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            isOnGoal = false;
        }
    }

    void Move()
    {
        if (isAttack || isDamaged)
            return;

        float horizontal = Input.GetAxisRaw("Horizontal"); // 좌:-1f, x:0f, 우:1f.

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
        if(Input.GetKeyDown(KeyCode.Space) && !isDamaged && !isAttack && jumpCount > 0)
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
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isAttack && !isDamaged && movement.isGround)
        {
            anim.SetTrigger("onAttack"); // onAttack을 트리거 하겠다.
            isAttack = true;
        }
    }
    void Goal()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && isOnGoal)
        {
            GameManager.Instance.OnStageClear();
            isStopControl = true;
            gameObject.SetActive(false);
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
    public void OnDamaged(Damageable.DamageMessage message)
    {
        Transform attaacker = message.attacker;

        // 공격자가 오른쪽에 있다면(포지션 x값이 나보다 크다면) 나는 왼쪽으로 날아간다.
        Vector2 dir = (attaacker.position.x >= transform.position.x) ? Vector2.left : Vector2.right;
        dir += Vector2.up;

        isDamaged = true;
        isAttack = false;

        movement.OnStop();               // movement 움직임 제어 중지.
        movement.OnKnockBack(dir, 4f);   // 실제로 넉백 시켜라.

        StartCoroutine("NoMovement");
        StartCoroutine("GodMode");
    }
    public void OnDead()
    {
        // 내 오브젝트를 꺼라.
        // gameObject.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Dead");

        anim.SetBool("isAlive", false);
        anim.SetTrigger("onDead");

        StopAllCoroutines();            // 모든 동작중인 코루틴 비활성화 (단, 이름으로 호출한 것)
        renderer.enabled = true;        // 랜더러 활성화.

        Time.timeScale = 0.3f;          // 시간의 흐름을 0.5배로 조정하라. (Default : 1)
        Invoke("OnFinishDead", 1.0f);   // N초 뒤에 해당 함수를 실행하라.
    }

    void OnFinishDead()
    {
        Time.timeScale = 1f;
    }
    bool IsDeadLine()
    {
        return transform.position.y <= -4f;
    }

    
    public void OnAddItem(ItemObject.ITEM item)
    {
        inventory.Add(item);
        ItemInfoUI.Instance.OnUpdateItem(item, inventory.Count(item));
    }

    IEnumerator GodMode()
    {
        gameObject.layer = LayerMask.NameToLayer("GodMode");
        float time = Time.time + godModeTime;
        float visibleTime = 0.0f;

        while (Time.time < time)
        {
            if(visibleTime <= Time.time)
            {
                renderer.enabled = !renderer.enabled;
                visibleTime = Time.time + 0.1f;
            }
                        
            yield return null;
        }

        renderer.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    IEnumerator NoMovement()
    {
        yield return new WaitForSeconds(0.1f);

        while (!movement.isGround)
            yield return null;

        isDamaged = false;
        movement.OnStart();
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
