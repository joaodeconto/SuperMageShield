using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _poolParent;
    [SerializeField] private List<GameObject> _pool;

    void Awake()
    {
        Instance = this;
    }

    public GameObject AvailableGameObject()
    {
        foreach (var item in _pool)
        {
            if(!item.gameObject.activeSelf)
                return item;
        }

        GameObject inst = Instantiate(_itemPrefab, _poolParent);
        _pool.Add(inst);        
        return inst;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
