using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    private void OnMouseDown()
    {
        int index = transform.GetSiblingIndex(); // 내가 몇번째 자식인지?
        StageManager.Instance.OnMoveStage(index);
    }
}
