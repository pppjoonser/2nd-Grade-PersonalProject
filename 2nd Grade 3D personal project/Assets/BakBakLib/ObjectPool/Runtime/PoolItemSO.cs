using UnityEngine;
namespace BakBak.ObjectPool.RunTime
{
    [CreateAssetMenu(fileName = "PoolItem", menuName = "SO/PoolItemSO", order = 0)]
    public class PoolItemSO : ScriptableObject
    {
        public string poolingName;
        public GameObject prefab;
        public int initCount;
    }
}