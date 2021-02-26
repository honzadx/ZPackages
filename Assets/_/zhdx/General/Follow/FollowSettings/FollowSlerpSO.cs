using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        [CreateAssetMenu(menuName = "zhdx/Follow/Slerp")]
        public class FollowSlerpSO : FollowSettingsSO
        {
            public float speed;

            public override void FollowPosition(float deltaTime, Transform follower, Transform target, float extraMtp = 1)
            {
                follower.position = Vector3.Slerp(follower.position, target.position, deltaTime * speed * extraMtp);
            }
            public override void FollowRotation(float deltaTime, Transform follower, Transform target, float extraMtp = 1)
            {
                follower.rotation = Quaternion.Slerp(follower.rotation, target.rotation, deltaTime * speed * extraMtp);
            }
        }
    }
}

