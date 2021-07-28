using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public struct DamageMessage
    {
        public Transform attacker;  // 공격자.
        public string attackerName; // 공격자의 이름.
        public int amount;          // 양.
    }

    // 피격 관리 클래스.
    [SerializeField] Transform infoPivot;                      // HP, 이름, 정보창의 기준점.
    [SerializeField] int maxHp;

    [SerializeField] UnityEvent<DamageMessage> OnDamagedEvent; // 매게변수 int형인 함수.
    [SerializeField] UnityEvent OnDeadEvent;                   // 매게변수 없는 함수.
    

    int hp;

    public Transform InfoPivot => infoPivot;
    public int MaxHp => maxHp;
    public int Hp => hp;

    void Start()
    {
        hp = maxHp;

        if(infoPivot != null)
            HpManager.Instance.Create(this);
    }

    public void OnDamaged(DamageMessage message)
    {
        Debug.Log(string.Format("{0}에게서 {1}의 데미지를 받음", 
            message.attackerName,
            message.amount
            ));

        OnDamagedEvent?.Invoke(message);    // 나에게 등록된 이벤트 호출.
        hp -= message.amount;               // 내 체력 감소.
        if (hp <= 0)
        {
            hp = 0;
            OnDead();
        }
    }
    void OnDead()
    {
        OnDeadEvent?.Invoke();       // 이벤트 델리게이트가 Null이 아니면 호출하라.
    }
}
