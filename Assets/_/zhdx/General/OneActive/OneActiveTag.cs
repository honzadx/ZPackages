using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx.General
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class OneActiveTag : MonoBehaviour
    {
        private OneActive origin;
        public void SetOrigin(OneActive origin) => this.origin = origin;

        private void OnEnable()
        {
            var siblingIndex = transform.GetSiblingIndex();

            origin = transform.parent.GetComponent<OneActive>();
            if (origin == null)
            {
                StripActiveTag();
                return;
            }

            origin.SetTagActive(siblingIndex);
        }

        public void StripActiveTag()
        {
            if (Application.isPlaying)
            {
                Destroy(this);
            }
            else
            {
                DestroyImmediate(this);
            }
        }
    }
}