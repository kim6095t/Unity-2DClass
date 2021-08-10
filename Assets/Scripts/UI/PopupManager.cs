using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : Singletone<PopupManager>
{
    [SerializeField] Transform panel;       // 패널.
    [SerializeField] Text titleText;        // 타이틀 텍스트.
    [SerializeField] Text context;          // 내용 텍스트.
    [SerializeField] Button leftButton;     // 왼쪽 버튼.
    [SerializeField] Button rightButton;    // 오른쪽 버튼.

    System.Action<int> OnCallback;          // 콜백.

    private void Start()
    {
        SwitchPopup(false);
        DontDestroyOnLoad(gameObject);
    }

    private void SwitchPopup(bool isOn)
    {
        panel.gameObject.SetActive(isOn);
    }

    public void OnSelectedPopup(int index)
    {
        OnCallback(index);
        SwitchPopup(false);
    }
    public void ShowPopup(string title, string context, string left, string right, System.Action<int> OnCallback)
    {
        titleText.text = title;
        this.context.text = context;
        leftButton.GetComponentInChildren<Text>().text = left;
        rightButton.GetComponentInChildren<Text>().text = right;                

        this.OnCallback = OnCallback;
        SwitchPopup(true);
    }
}
