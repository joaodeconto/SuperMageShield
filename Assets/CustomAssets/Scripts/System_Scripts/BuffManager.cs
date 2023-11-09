using SuperMageShield;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [SerializeField] private List<BuffSO> buffList = new List<BuffSO>();

    public static BuffManager Instance;
    private float _randomBuff;

    private void Awake()
    {
        Instance = this;
    }
    public void DropBuff(Vector2 position)
    {
        _randomBuff = Random.Range(0, buffList.Count);
        Instantiate(buffList[(int)_randomBuff].entityPrefab, position, Quaternion.identity);
    }
}
