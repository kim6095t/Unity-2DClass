using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    enum TITLE_BUTTON
    {
        NewGame,
        Continue,
        Option,
        Exit,

        Count,
    }

    [SerializeField] ButtonSwitch[] buttons;

    private void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.BGM.Forest1);        // 배경음 재생.

        bool isSavedData = DataManager.IsSavedData;
        buttons[(int)TITLE_BUTTON.Continue].Switch(isSavedData);
    }

    void OnNewGameAsDelete()
    {
        DataManager.DeleteAll();
        DataManager.SaveAll();
        SceneMover.Instance.MoveScene("WorldMap");
    }

    public void NewGame()
    {
        if(DataManager.IsSavedData)
        {
            // 세이브 파일 존재. 삭제할 것인지 물어본다.
            Debug.Log("진짜 지웁니까?");

            PopupManager.Instance.ShowPopup(
                "새 게임 시작",
                "기존의 데이터를 삭제합니다.\n정말 삭제하시겠습니까?",
                "그래요", "싫어요",
                (index) => {  
                    if(index == 0)
                        OnNewGameAsDelete();
                });
        }
        else
        {
            DataManager.SaveAll();
            SceneMover.Instance.MoveScene("WorldMap");
        }                
    }
    public void Continue()
    {
        SceneMover.Instance.MoveScene("WorldMap");
    }
    public void OpenOption()
    {
        SceneMover.Instance.OpenOption();
    }
    public void ExitGame()
    {
        Debug.Log("ExitGame");
    }
}
