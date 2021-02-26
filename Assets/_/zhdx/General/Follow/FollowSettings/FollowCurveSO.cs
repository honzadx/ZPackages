using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General 
    {
        [CreateAssetMenu(menuName = "zhdx/Follow/Curve")]
        public class FollowCurveSO : FollowSettingsSO
        {
            public float speed;
            public AnimationCurve curve;

            private Vector3 CalculateDistanceV3(Transform follower, Transform target) => target.position - follower.position;
            private float CalculateAmount(float deltaTime, Vector3 distanceV3, float extraMtp) => curve.Evaluate(distanceV3.sqrMagnitude);

            public override void FollowPosition(float deltaTime, Transform follower, Transform target, float extraMtp = 1)
            {
                var distanceV3 = CalculateDistanceV3(follower, target);
                var movement = CalculateAmount(deltaTime, distanceV3, extraMtp);
                follower.position = Vector3.MoveTowards(follower.position, target.position, movement * speed * deltaTime * extraMtp);
            }
            public override void FollowRotation(float deltaTime, Transform follower, Transform target, float extraMtp = 1)
            {
                var distanceV3 = CalculateDistanceV3(follower, target);
                var rotation = CalculateAmount(deltaTime, distanceV3, extraMtp);
                follower.rotation = Quaternion.RotateTowards(follower.rotation, target.rotation, rotation * speed * deltaTime * extraMtp);
            }
        }
    }
}
