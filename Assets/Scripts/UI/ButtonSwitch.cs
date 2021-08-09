using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{    
    Color ENABLE_COLOR;             // 활성화 색상 (원래 본연의 색상)
    Color DISABLE_COLOR;            // 비활성화 색상.

    [SerializeField] Image buttonImage;
    [SerializeField] Button button;

    private void Start()
    {
        ENABLE_COLOR = buttonImage.color;
        DISABLE_COLOR = new Color(.5f, .5f, .5f, 1f);
    }
    public void Switch(bool isOn)
    {
        buttonImage.color = isOn ? ENABLE_COLOR : DISABLE_COLOR;
        button.enabled = isOn;
    }
}
