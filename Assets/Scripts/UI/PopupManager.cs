using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : Singletone<PopupManager>
{
    public enum BUTTON_TYPE
    {
        Confirm,        // 확인. 버튼 1개.
        Choice,         // 선택. 버튼 2개.
        Triple,         // 3가지.
    }

    public struct ButtonHandle
    {
        public string[] buttonNames;
        public BUTTON_TYPE type;

        public ButtonHandle(string one, string two, string three)
        {
            buttonNames = new string[3];
            buttonNames[0] = one;
            buttonNames[1] = two;
            buttonNames[2] = three;
            type = BUTTON_TYPE.Triple;
        }

        public ButtonHandle(string one, string two)
            : this(one, two, string.Empty)
        {
            type = BUTTON_TYPE.Choice;
        }

        public ButtonHandle(string one)
            : this(one, string.Empty, string.Empty)
        {
            type = BUTTON_TYPE.Confirm;
        }
    }


    [SerializeField] Transform panel;       // 패널.
    [SerializeField] Text titleText;        // 타이틀 텍스트.
    [SerializeField] Text context;          // 내용 텍스트.
    [SerializeField] Button[] buttons;      // 버튼 배열.

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


    public void ShowPopup(string title, string context, ButtonHandle handle, System.Action<int> OnCallback)
    {
        titleText.text = title;
        this.context.text = context;

        int buttonCount = (int)handle.type;       // 전달 받은 버튼 개수.
        for(int i = 0; i<buttons.Length; i++)     // 전체 버튼 배열을 순회.
        {
            buttons[i].gameObject.SetActive(i <= buttonCount);          // 버튼을 끄고 켠다.
            buttons[i].gameObject.GetComponentInChildren<Text>().text = handle.buttonNames[i];      // 버튼의 텍스트 대입.
        }

        this.OnCallback = OnCallback;
        SwitchPopup(true);
    }
}
