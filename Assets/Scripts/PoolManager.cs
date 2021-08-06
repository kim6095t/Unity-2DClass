using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool<T>
{
    void Setup(System.Action<T> OnReturnPool);
}

public class PoolManager<TargetObject, PoolObject> : Singletone<TargetObject>
    where TargetObject : MonoBehaviour
    where PoolObject : MonoBehaviour, IPool<PoolObject>  // 제네릭 자료형 PoolObject는 MonoBehaviour를 상속하고 있어야 한다.
{
    [SerializeField] PoolObject poolPrefab;     // 풀링할 오브젝트 프리팹.
    [SerializeField] int initCount;             // 초기 생성 개수.

    Stack<PoolObject> storage;                  // 저장소.

    private new void Awake()
    {
        base.Awake();

        storage = new Stack<PoolObject>();      // stack 변수 객체 생성.
        for (int i = 0; i < initCount; i++)
            CreatePool();
    }

    private void CreatePool()
    {
        PoolObject pool = Instantiate(poolPrefab, transform);
        pool.Setup(OnReturnPool);
        pool.gameObject.SetActive(false);
        storage.Push(pool);
    }

    protected PoolObject GetPool()
    {
        if (storage.Count <= 0)
            CreatePool();

        storage.Peek().gameObject.SetActive(true);
        return storage.Pop();
    }

    private void OnReturnPool(PoolObject pool)
    {
        pool.gameObject.SetActive(false);                       // 돌아온 pool을 끈다.
        storage.Push(pool);                                     // 저장소에 push한다.
    }
}
