using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    private void OnMouseUpAsButton()             // 내가 같은 지점에서 누르고 땠을 때.
    {
        int index = transform.GetSiblingIndex(); // 내가 몇번째 자식인지?
        StageManager.Instance.OnMoveStage(index);
    }
}
