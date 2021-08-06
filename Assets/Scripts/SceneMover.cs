using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMover : Singletone<SceneMover>
{
    [SerializeField] Image blindImage;      // 가림막.
    
    void Start()
    {
        blindImage.enabled = false;
        DontDestroyOnLoad(gameObject);
    }

    bool isMoving;      // 씬 로딩 여부.
    bool isOpenOption;  // 옵션 창 켜짐 여부.

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenOption();
        }
    }

    public void MoveScene(string sceneName)
    {
        // 이동 중에는 요청을 막는다.
        if (isMoving)
            return;

        isMoving = true;
        StartCoroutine(MoveTo(sceneName));      // 씬 이동 코루틴 호출.
    }
    public void OpenOption()
    {
        if (!isOpenOption)
        {
            isOpenOption = true;
            OptionManager.OnExit += () => { isOpenOption = false; };
            SceneManager.LoadScene("Option", LoadSceneMode.Additive);
        }
    }

    IEnumerator MoveTo(string sceneName)
    {
        yield return StartCoroutine(FadeOut()); // FadeOut 코루틴이 끝날때까지 기다린다.
        SceneManager.LoadScene(sceneName);

        bool isLoading = true;

        // 이벤트 등록. 로딩이 끝나는 지점에 호출.
        // 람다식으로 이벤트에 등록.
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => {
            isLoading = false;
        };

        // isLoading이 true일 경우 무한 대기.
        while (isLoading)
            yield return null;

        // 씬 로딩이 끝남.
        yield return StartCoroutine(FadeIn());  // FadeIn 코루틴이 끝날때까지 기다린다.
        isMoving = false;
    }

    IEnumerator FadeOut()           // 화면이 점차 검정색으로.
    {
        blindImage.enabled = true;
        blindImage.color = new Color(0, 0, 0, 0);   // 검은색, 투명도가 0.

        const float fadeTime = 1.0f;
        float time = 0.0f;

        while(true)
        {
            time += Time.deltaTime;
            if (time >= fadeTime)
                break;

            blindImage.color = new Color(0, 0, 0, time / fadeTime);
            yield return null;
        }

        blindImage.color = Color.black;
    }
    IEnumerator FadeIn()        // 화면이 점차 원래대로.
    {
        blindImage.enabled = true;
        blindImage.color = new Color(0, 0, 0, 0);   // 검은색, 투명도가 0.

        const float fadeTime = 1.0f;
        float time = fadeTime;

        while (true)
        {
            time -= Time.deltaTime;
            if (time <= 0.0f)
                break;

            blindImage.color = new Color(0, 0, 0, time / fadeTime);
            yield return null;
        }

        blindImage.color = new Color(0, 0, 0, 0);
        blindImage.enabled = false;
    }
    
}
