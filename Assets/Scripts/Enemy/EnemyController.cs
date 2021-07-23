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
    bool isAlive;

    private void Start()
    {
        horizontal = 1f;
        isAlive = true;

        // 키 = headPivot과 Enemy의 발(pivot) 까지의 거리.
        height = Vector2.Distance(headPivot.position, transform.position);
    }

    void Update()
    {
        if (!isAlive)
            return;

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


    public void OnDamaged(Damageable.DamageMessage message)
    {
        // 반짝임.
        StartCoroutine(OnBlink());

        Vector2 dir = (transform.position.x <= message.attacker.position.x) ? Vector2.left : Vector2.right;
        movement.OnKnockBack(dir, 2);
    }
    IEnumerator OnBlink()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        renderer.color = Color.white;
    }

    public void OnDead()
    {
        // 내가 죽었다.
        anim.SetTrigger("onDead");

        isAlive = false;
        gameObject.layer = LayerMask.NameToLayer("Dead");   // 레이어를 Dead로 바꾼다.
        StartCoroutine(OnDisappear());
    }

    IEnumerator OnDisappear()
    {
        yield return new WaitForSeconds(3f);
        float time = 1f;

        while(true)
        {
            yield return null; // 1프레임 쉬겠다.

            Color originColor = renderer.color;
            originColor.a = time / 1f;
            renderer.color = originColor;

            time -= Time.deltaTime;
            if (time <= 0.0f)
                break;
        }

        renderer.color = new Color(0f, 0f, 0f, 0f);
        gameObject.SetActive(false);
    }
}
