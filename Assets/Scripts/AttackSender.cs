using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSender : MonoBehaviour
{
    [SerializeField] PlayerController movement;
    [SerializeField] AudioSource attackSFX1;
    [SerializeField] AudioSource attackSFX2;

    public void OnFinishAttack()
    {
        movement.OnFinishAttack();
    }
    public void OnAttack1()
    {
        attackSFX1.Play();
    }
    public void OnAttack2()
    {
        attackSFX2.Play();
    }
}
