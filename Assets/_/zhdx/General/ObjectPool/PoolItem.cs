using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        public class PoolItem : MonoBehaviour
        {
            private ObjectPool origin;
            public void SetOrigin(ObjectPool origin) => this.origin = origin;

            private void OnDisable()
            {
                if (gameObject != null && origin != null)
                {
                    origin.ReturnItem(this);
                }
            }
        }
    }
}