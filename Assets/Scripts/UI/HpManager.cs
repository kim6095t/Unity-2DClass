using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    static HpManager instance;
    public static HpManager Instance => instance;

    [SerializeField] HpBar prefab;

    private void Awake()
    {
        instance = this;
    }

    public void Create(Damageable target)
    {
        HpBar clone = Instantiate(prefab, transform);
        clone.Setup(target);
    }
    
}
