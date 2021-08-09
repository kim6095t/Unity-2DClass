using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] PlayerInfoUI playerUI;
    [SerializeField] GameOver gameOverUI;
    [SerializeField] string playerName;
    [SerializeField] int hp;

    private void Start()
    {
        playerUI.SetName(playerName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnDamaged();
    }

    public void OnDamaged()
    {
        // 누군가에게 공격을 받았다.
        hp -= 1;
        playerUI.UpdateHp(hp);
        if (hp <= 0)
            OnDead();
    }

    void OnDead()
    {
        gameOverUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
   
}
