using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStage : MonoBehaviour
{
    [SerializeField] Transform playerStart;
    [SerializeField] Animation doorAnim;

    void Start()
    {
        GameManager.Instance.OnOpenDoor += OnOpenDoor;
        PlayerController.Instance.transform.position = playerStart.position;
    }

    void OnOpenDoor()
    {
        doorAnim.Play();
    }
    
}
