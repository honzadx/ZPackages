using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx.General
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField]
        private PoolItem prefab = null;

        [SerializeField]
        private int size = 10;
        [SerializeField]
        private int autoIncrease = 5;

        private List<PoolItem> items;
        private List<PoolItem> active;

        private void Start()
        {
            active = new List<PoolItem>();
            items = new List<PoolItem>();
            FillPool();
        }

        public void FillPool()
        {
            for (int i = 0; i < size; ++i)
            {
                AddItem();
            }
        }

        public PoolItem IncreasePoolSize(int increase)
        {
            PoolItem item = null;
            for (int i = 0; i < increase; ++i)
            {
                item = AddItem();
            }
            size += increase;
            return item;
        }

        public PoolItem AddItem()
        {
            var item = Instantiate(prefab, transform);
            item.gameObject.SetActive(false);
            item.SetOrigin(this);
            items.Add(item);
            return item;
        }

        public void ClearPool()
        {
            items = null;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public PoolItem TakeItem()
        {
            if (items.Count > 0)
            {
                var index = items.Count - 1;
                var item = items[index];
                items.Remove(item);
                active.Add(item);
                return item;
            }
            else
            {
                var item = IncreasePoolSize(autoIncrease);
                items.Remove(item);
                active.Add(item);
                return item;
            }
        }

        public void ReturnItem(PoolItem item)
        {
            active.Remove(item);
            if (!items.Contains(item))
                items.Add(item);
        }
    }
}