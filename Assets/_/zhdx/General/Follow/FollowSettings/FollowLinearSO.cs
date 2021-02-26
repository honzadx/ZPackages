using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        [CreateAssetMenu(menuName = "zhdx/Follow/Linear")]
        public class FollowLinearSO : FollowSettingsSO
        {
            public float movementSpeed;
            public float rotationSpeed;

            public override void FollowPosition(float deltaTime, Transform follower, Transform target, float extraMtp = 1)
            {
                var distanceV3 = target.position - follower.position;
                var dir = distanceV3.normalized;
                var movement = dir * deltaTime * movementSpeed * extraMtp;
                if (movement.sqrMagnitude > distanceV3.sqrMagnitude)
                {
                    follower.position = target.position;
                }
                else
                {
                    follower.position += movement;
                }
            }

            public override void FollowRotation(float deltaTime, Transform follower, Transform target, float extraMtp = 1)
            {
                var rotation = deltaTime * rotationSpeed * extraMtp;
                follower.rotation = Quaternion.RotateTowards(follower.rotation, target.rotation, rotation);
            }
        }
    }
}
