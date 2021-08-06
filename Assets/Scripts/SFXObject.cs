using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AudioSource라는 컴퍼넌트가 없으면 해당 스크립트를 Add 할 수 없다.
// 해당 스크립트가 존재하는 한 AudioSource 컴퍼넌트는 Remove할 수 없다.
[RequireComponent(typeof(AudioSource))]
public class SFXObject : MonoBehaviour, IPool<SFXObject>
{
    AudioSource speaker;
    System.Action<SFXObject> OnReturnPool;

    public void Setup(Action<SFXObject> OnReturnPool)
    {
        this.OnReturnPool = OnReturnPool;               // 저장소로 돌아가는 델리게이트 대입.
        speaker = GetComponent<AudioSource>();          // 내 오브젝트에서 AudioSource 컴퍼넌트 검색 후 대입.
    }
    public void PlaySFX(AudioClip clip, float volumn)
    {
        speaker.clip = clip;                            // 내 스피커 안에 매게변수 clip을 삽입.
        speaker.volume = volumn;                        // 스피커의 볼륨을 조절.
        speaker.Play();                                 // 효과음 재생.

        StartCoroutine(CheckPlaying());                 // 코루틴 호출.
    }

    

    IEnumerator CheckPlaying()
    {
        while (speaker.isPlaying)                       // 스피커가 재생 중일 경우 참.
            yield return null;

        //Destroy(gameObject);                          // 나 자신을 삭제.
        OnReturnPool?.Invoke(this);                     // 나 자신을 저장소로 반환.
    }
}
