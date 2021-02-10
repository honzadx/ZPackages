using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx.General
{
    public class OpenTranslate : MonoBehaviour
    {
        [SerializeField]
        private Vector3 dir = Vector3.zero;
        [SerializeField]
        private float speed = 1;
        [SerializeField]
        private Space relativeTo = Space.Self;

        private void Start()
        {
            dir.Normalize();
        }

        private void Update()
        {
            transform.Translate(dir * speed * Time.deltaTime, relativeTo);
        }
    }
}