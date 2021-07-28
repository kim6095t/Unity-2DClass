using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Image hpImage;
    [SerializeField] Damageable target;

    Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (target.Hp <= 0)
            gameObject.SetActive(false);

        // 위치 갱신.
        // 월드 위치를 화면 위치 좌표로 변환.
        if (target.InfoPivot != null)
            transform.position = cam.WorldToScreenPoint(target.InfoPivot.position);
        else
            transform.position = cam.WorldToScreenPoint(target.transform.position);

        SetHp();
    }

    public void Setup(Damageable target)
    {
        this.target = target;
    }
    private void SetHp()
    {
        // int / int = int
        // float / int = float
        // int / float = float
        // float / float = float

        // 0.0f ~ 1.0f
        hpImage.fillAmount = target.Hp / (float)target.MaxHp;
    }
}
