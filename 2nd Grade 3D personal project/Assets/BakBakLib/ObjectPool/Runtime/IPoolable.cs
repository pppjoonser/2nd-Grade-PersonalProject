using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BakBak.ObjectPool.RunTime
{
    public interface IPoolable
    {
        public PoolItemSO PoolItem { get; }
        public GameObject GameObject { get; }

        public void SetUpPool(Pool pool);
        public void ResetItem();
    }
}
