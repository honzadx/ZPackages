using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        public class AverageMovement : MonoBehaviour
        {
            [SerializeField]
            private List<Transform> targets = null;

            private Transform _transform;

            private void Awake()
            {
                _transform = GetComponent<Transform>();
            }

            private void Update()
            {
                _transform.position = AveragePosition();
                _transform.rotation = AverageRotation();
            }

            private Vector3 AveragePosition()
            {
                var pos = Vector3.zero;
                foreach(Transform target in targets)
                {
                    pos += target.position;
                }

                return pos / targets.Count;
            }

            private Quaternion AverageRotation()
            {
                var rot = Quaternion.identity;
                for (int i = 0; i < targets.Count; ++i)
                {
                    if(i == 0)
                    {
                        rot = targets[i].rotation;
                    }
                    else
                    {
                        rot = Quaternion.Slerp(rot, targets[i].rotation, 1.0f/(i + 1.0f));
                    }
                }

                return rot;
            }
        }
    }
}