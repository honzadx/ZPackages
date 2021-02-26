using UnityEngine;

namespace zhdx
{
    namespace General
    {
        public abstract class FollowSettingsSO : ScriptableObject
        {
            public abstract void FollowPosition(float deltaTime, Transform follower, Transform target, float extraMtp = 1);
            public abstract void FollowRotation(float deltaTime, Transform follower, Transform target, float extraMtp = 1);
        }
    }
}