using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : PoolManager<SoundManager, SFXObject>
{
    public enum BGM
    {
        Forest1,
    }

    [SerializeField] AudioSource audioSource;                   // 스피커.
    [SerializeField] AudioClip[] bgms;                          // 배경음악 오디오 파일.
    [SerializeField] AudioClip[] sfxs;                          // 효과음 오디오 파일.

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
        if (isSaved == false)
        {
            // 초기 값.
            bgmVolume = GameData.MAX_BGM_VOLUMN;
            sfxVolume = GameData.MAX_SFX_VOLUMN;
        }

        audioSource.volume = bgmVolume;             // 스피커 사운드 조절.
        DontDestroyOnLoad(gameObject);              // 씬이 로드가 되어도 Destory하지 마라.
    }


    public void PlayBGM(BGM bgm)
    {
        PlayBGM(bgm.ToString());
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

    public void PlaySFX(string sfxName)
    {
        for (int i = 0; i < sfxs.Length; i++)
        {
            if(sfxs[i].name == sfxName)                             // 요청하는 이름과 같은 효과음이 있을 경우.
            {
                SFXObject sfx = GetPool();                          // 효과음 프리팹을 저장소에서 꺼내옴.
                sfx.PlaySFX(sfxs[i], sfxVolume);                    // 효과음 재생.
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

    public void OnSave()
    {
        Debug.Log("Sound Manager Saved!!");

        DataManager.SetFloat(KEY_BGM_VOLUMNE, bgmVolume);
        DataManager.SetFloat(KEY_SFX_VOLUMNE, sfxVolume);
        DataManager.SetBool(KEY_SOUND_MANAGER, true);
    }
    public void OnLoad()
    {
        bgmVolume = DataManager.GetFloat(KEY_BGM_VOLUMNE);
        sfxVolume = DataManager.GetFloat(KEY_SFX_VOLUMNE);
        isSaved   = DataManager.GetBool(KEY_SOUND_MANAGER);
    }
}
