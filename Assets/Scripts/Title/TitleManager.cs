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
            PopupManager.Instance.ShowPopup(
                "새 게임 시작",
                "기존의 데이터를 삭제합니다.\n정말 삭제하시겠습니까?",
                new PopupManager.ButtonHandle("아니오", "예"),
                (index) => {  
                    if(index == 1)
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
        PopupManager.Instance.ShowPopup("이어하기", "게임을 이어합니다.",
            new PopupManager.ButtonHandle("GO!"),
            (index) => {

                SceneMover.Instance.MoveScene("WorldMap");
            });
    }
    public void OpenOption()
    {
        SceneMover.Instance.OpenOption();
    }
    public void ExitGame()
    {
        PopupManager.Instance.ShowPopup("게임 종료", "게임을 종료하시겠습니까?",
            new PopupManager.ButtonHandle("예", "네", "끌게요"),
            (index) => {

                Debug.Log("선택된 버튼은 : " + index);
            });
    }
}
