using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKey : MonoBehaviour
{
    public event System.Action OnGet;

    // 오브젝트가 Destory되었을 때 호출되는 함수.
    private void OnDestroy()
    {
        OnGet?.Invoke();
    }
}
