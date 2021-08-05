using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singletone<SoundManager>
{
    [SerializeField] AudioSource audioSource;                   // 스피커.
    [SerializeField] AudioClip[] bgms;                          // 실제 오디오 파일 배열.

    private const string KEY_BGM_VOLUMNE = "bgmVolumn";         // 배경음 크기 키 값.
    private const string KEY_SFX_VOLUMNE = "sfxVolumn";         // 효과음 크기 키 값.
    private const string KEY_SOUND_MANAGER = "SoundManager";    // 사운드 매니저 트리거.

    private bool isSaved;                 // 저장 여부.

    private float bgmVolume;              // 배경음 크기.
    private float sfxVolume;              // 효과음 크기.

    public float BgmVolumn => bgmVolume;  // 배경음 크기 프로퍼티 (읽기 전용)
    public float SfxVolumn => sfxVolume;  // 효과음 크기 프로퍼티 (읽기 전용)

    private void Start()
    {
        DataManager.OnSave += OnSave;
        DataManager.OnLoad += OnLoad;

        OnLoad();

        // 최초에 실행이 되었을 때.
        if(isSaved == false)
        {
            // 초기 값.
            bgmVolume = GameData.MAX_BGM_VOLUMN;
            sfxVolume = GameData.MAX_SFX_VOLUMN;
        }

        DontDestroyOnLoad(gameObject);              // 씬이 로드가 되어도 Destory하지 마라.
    }

    public void PlayBGM(string bgmName)
    {
        for (int i = 0; i < bgms.Length; i++)       // 모든 BGM 배열을 순회.
        {
            if(bgms[i].name == bgmName)             // i번째 BGM의 이름이 같다면
            {
                audioSource.clip = bgms[i];         // audioSource(스피커)에 clip(CD)를 대입.
                audioSource.Play();                 // 재생 버튼.
                break;
            }
        }
    }

    public void OnChangedVolumnBGM(float volume)
    {
        bgmVolume = volume;
        audioSource.volume = bgmVolume / GameData.MAX_BGM_VOLUMN;
    }
    public void OnChangedVolumnSFX(float volume)
    {
        sfxVolume = volume;
    }

    void OnSave()
    {
        DataManager.SetFloat(KEY_BGM_VOLUMNE, bgmVolume);
        DataManager.SetFloat(KEY_SFX_VOLUMNE, sfxVolume);
        DataManager.SetBool(KEY_SOUND_MANAGER, true);
    }
    void OnLoad()
    {
        bgmVolume = DataManager.GetFloat(KEY_BGM_VOLUMNE);
        sfxVolume = DataManager.GetFloat(KEY_SFX_VOLUMNE);
        isSaved   = DataManager.GetBool(KEY_SOUND_MANAGER);
    }
}
