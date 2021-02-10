using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx.General
{
    public class OpenTransform : MonoBehaviour
    {
        public void SetPosition(Transform target)
        {
            transform.position = target.position;
        }

        public void SetRotation(Transform target)
        {
            transform.rotation = target.rotation;
        }

        public void SetTransform(Transform target)
        {
            SetPosition(target);
            SetRotation(target);
        }
    }
}