using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        [ExecuteAlways]
        public class OneActive : MonoBehaviour
        {
            public void Awake()
            {
                for (int i = 0; i < transform.childCount; ++i)
                {
                    var child = transform.GetChild(i).gameObject;
                    if (!child.GetComponent<OneActiveTag>())
                    {
                        child.AddComponent<OneActiveTag>();
                        var tag = child.GetComponent<OneActiveTag>();
                    }
                }

                SetTagActive(0);
            }

            public void SetTagActive(int siblingIndex)
            {
                for (int i = 0; i < transform.childCount; ++i)
                {
                    if (i != siblingIndex)
                    {
                        transform.GetChild(i).gameObject.SetActive(false);
                    }
                    else
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
            }

            public void ActivateNext()
            {
                int index = GetCurrentTagIndex();
                index = index + 1 >= transform.childCount ? 0 : index + 1;
                SetTagActive(index);
            }

            public void ActivatePrevious()
            {
                int index = GetCurrentTagIndex();
                index = index - 1 < 0 ? transform.childCount - 1 : index + -1;
                SetTagActive(index);
            }

            public int GetCurrentTagIndex()
            {
                for (int i = 0; i < transform.childCount; ++i)
                {
                    if (transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        return i;
                    }
                }
                return 0;
            }
        }
    }
}