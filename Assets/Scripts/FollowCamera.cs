using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;      // 타겟.
    [SerializeField] Vector3 offset;        // 간격.
    [SerializeField] float moveSpeed;       // 카메라의 속도.

    // Update() 호출 후에 LateUpdate()가 불린다.
    // 이유는 타겟이 Update문에서 위치를 수정하고
    // 그 후에 내가 따라가야하기 때문에.
    void LateUpdate()
    {
        // 내 위치를 타겟의 위치로 한다.

        transform.position = target.position + offset;

        /*
        transform.position = Vector3.Lerp(
            transform.position, 
            target.position + offset, 
            moveSpeed * Time.deltaTime);
        */
    }
}
