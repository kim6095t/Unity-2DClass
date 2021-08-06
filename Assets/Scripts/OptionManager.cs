using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;

    public static event System.Action OnExit;

    SoundManager soundManager = null;
    
    void Start()
    {
        soundManager = SoundManager.Instance;

        bgmSlider.maxValue = GameData.MAX_BGM_VOLUMN;   // 슬라이더 바의 최대 값을 게임 데이터 값으로 대입.
        sfxSlider.maxValue = GameData.MAX_SFX_VOLUMN;   // 슬라이더 바의 최대 값을 게임 데이터 값으로 대입.

        bgmSlider.value = soundManager.BgmVolumn;       // 슬라이더 바의 초기 값을 사운드 매니저에서 가져와 대입.
        sfxSlider.value = soundManager.SfxVolumn;       // 슬라이더 바의 초기 값을 사운드 매니저에서 가져와 대입.
    }

    public void OnChangedVolumeBGM(float volumn)
    {
        soundManager.OnChangedVolumnBGM(volumn);
    }
    public void OnChangedVolumeSFX(float volumn)
    {
        soundManager.OnChangedVolumnSFX(volumn);
    }

    public void OnExitOption()
    {
        OnExit?.Invoke();           // 창이 꺼질때 등록된 이벤트 호출.
        OnExit = null;              // 등록된 이벤트 모두 삭제.

        soundManager.OnSave();      // 옵션 저장.

        // 현재 씬을 지운다.
        SceneManager.UnloadSceneAsync("Option");
    }
}
