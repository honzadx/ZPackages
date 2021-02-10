using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx.General
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform follower;
        public Transform target;
        public FollowSettingsSO followPositionSettings;
        public FollowSettingsSO followRotationSettings;
        public float extraMtp = 1;

        private void Start()
        {
            if(follower == null)
            {
                follower = transform;
            }
        }

        private void Update()
        {
            followPositionSettings?.FollowPosition(Time.deltaTime, follower, target, extraMtp);
            followRotationSettings?.FollowRotation(Time.deltaTime, follower, target, extraMtp);
        }
    }
}
