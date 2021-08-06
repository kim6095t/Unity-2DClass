using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXHandler : MonoBehaviour
{
    [SerializeField] string sfxName;

    private void Start()
    {
        Button button = GetComponent<Button>();                 // 나에게서 Button 검색
        if(button == null)                                      // 버튼이 없을 경우.
        {
            Debug.Log(name + " is not have button!");           // 로그 출력.
            enabled = false;                                    // 스크립터 끄기.
            return;
        }

        button.onClick.AddListener(OnPlaySFX);                  // 버튼 클릭 이벤트 onClick에 이벤트 등록.
    }

    public void OnPlaySFX()
    {
        SoundManager.Instance.PlaySFX(sfxName);
    }
}
