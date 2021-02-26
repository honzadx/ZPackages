using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        public class FollowTarget : MonoBehaviour
        {
            public Transform follower;
            public Transform target;
            public FollowSettingsSO followPositionSettings;
            public FollowSettingsSO followRotationSettings;
            public float positionMtp = 1;
            public float rotationMtp = 1;

            private void Awake()
            {
                if (follower == null)
                {
                    follower = transform;
                }
            }

            private void Update()
            {
                followPositionSettings?.FollowPosition(Time.deltaTime, follower, target, positionMtp);
                followRotationSettings?.FollowRotation(Time.deltaTime, follower, target, rotationMtp);
            }
        }
    }
}
