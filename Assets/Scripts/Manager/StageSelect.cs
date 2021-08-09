using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    [SerializeField] Collider2D selectCollider;
    [SerializeField] GameObject lockStage;
    [SerializeField] GameObject lockImage;

    int stageIndex;

    private void Start()
    {
        stageIndex = transform.GetSiblingIndex(); // 내가 몇번째 자식인지?
        bool isUnlock = PlayerData.Instance.isUnlockStages[stageIndex];

        selectCollider.enabled = isUnlock;
        lockStage.SetActive(!isUnlock);
        lockImage.SetActive(!isUnlock);
    }

    private void OnMouseUpAsButton()              // 내가 같은 지점에서 누르고 땠을 때.
    {
        StageManager.Instance.OnMoveStage(stageIndex);
    }
}
