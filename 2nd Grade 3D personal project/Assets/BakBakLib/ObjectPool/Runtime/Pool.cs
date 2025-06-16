﻿using BakBak.ObjectPool.RunTime;
using System.Collections.Generic;
using UnityEngine;

namespace BakBak.ObjectPool.RunTime
{
    public class Pool
    {
        private readonly Stack<IPoolable> _pool;
        private readonly Transform _parentTrm;
        private readonly GameObject _prefab;

        public Pool(IPoolable poolable, Transform parentTrm, int count)
        {
            _pool = _pool = new Stack<IPoolable>(count);
            _parentTrm = parentTrm;
            _prefab = poolable.GameObject;

            for (int i = 0; i < count; i++)
            {
                GameObject gameObj = GameObject.Instantiate(_prefab, _parentTrm);
                gameObj.SetActive(false);
                IPoolable item = gameObj.GetComponent<IPoolable>();
                item.SetUpPool(this);
                _pool.Push(item);
            }
        }

        public IPoolable Pop()
        {
            IPoolable item;
            if (_pool.Count == 0)
            {
                GameObject gameObj = GameObject.Instantiate(_prefab, _parentTrm);
                item = gameObj.GetComponent<IPoolable>();
                item.SetUpPool(this);
            }
            else
            {
                item = _pool.Pop();
                item.GameObject.SetActive(true);
            }
            item.ResetItem();
            return item;
        }

        public void Push(IPoolable item)
        {
            item.GameObject.SetActive(false);
            _pool.Push(item);
        }
    }
}